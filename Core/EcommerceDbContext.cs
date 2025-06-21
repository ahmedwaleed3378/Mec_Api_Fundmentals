using Mec_Api_Fundmentals.Models;
using Microsoft.EntityFrameworkCore;

namespace Mec_Api_Fundmentals.Core
{
	public class EcommerceDbContext : DbContext
	{
		public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) { }

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

	}
}
