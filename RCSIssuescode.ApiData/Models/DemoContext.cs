using System.Data.Entity;

namespace RCSIssues.ApiData.Models
{
	public class DemoContext : DbContext
	{

		public DbSet<Customer> Customers { get; set; }

		public DemoContext()
			: base("DemoContext")
        {
        }
	}

}