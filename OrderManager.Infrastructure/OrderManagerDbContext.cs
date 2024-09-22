using Microsoft.EntityFrameworkCore;
using OrderManager.Core.Entities;
using System.Reflection;

namespace OrderManager.Infrastructure
{
	public class OrderManagerDbContext : DbContext
	{
        public OrderManagerDbContext(DbContextOptions<OrderManagerDbContext> options) : base(options)
		{
            
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	} 
}
