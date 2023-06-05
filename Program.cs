using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectContext>(opt => opt.UseInMemoryDatabase("Projects"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/todoitems", async (ProjectContext db) =>
    await db.Projects.ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, ProjectContext db) =>
await db.Projects.FindAsync(id)
    is Project project
        ? Results.Ok(project)
        : Results.NotFound());

app.MapPost("/todoitems", async (Project project, ProjectContext db) =>
{
    db.Projects.Add(project);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{project.Id}", project);
});

app.MapPut("/todoitems/{id}", async (int id, Project inputProject, ProjectContext db) =>
{
    var project = await db.Projects.FindAsync(id);

    if (project is null) return Results.NotFound();

    project.ResponsiblePerson = inputProject.ResponsiblePerson;
    project.Success = inputProject.Success;
    project.Description = inputProject.Description;
    project.date = inputProject.date;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, ProjectContext db) =>
{
    if (await db.Projects.FindAsync(id) is Project project)
    {
        db.Projects.Remove(project);
        await db.SaveChangesAsync();
        return Results.Ok(project);
    }

    return Results.NotFound();
});

app.Run();

