using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;

namespace MyWebApi.Models
{
	public class ProjectContext : DbContext
		
	{
		public DbSet<Project> Projects { get; set; }
		public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
