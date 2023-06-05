using System;
namespace MyWebApi.Models
{
	public class Project
	{
		public long Id { get; set; }
		public string ResponsiblePerson { get; set; }
		public DateOnly date { get; set; }
		public double Success { get; set; }
		public string Description { get; set; }
	}
}