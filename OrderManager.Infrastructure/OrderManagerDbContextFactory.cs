using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OrderManager.Infrastructure
{
	public class OrderManagerDbContextFactory : IDesignTimeDbContextFactory<OrderManagerDbContext>
	{
		public OrderManagerDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<OrderManagerDbContext>();

			var configuration = new ConfigurationBuilder()
					   .SetBasePath(Directory.GetCurrentDirectory() + "/../OrderManager.API")
					   .AddJsonFile("appsettings.json")
					   .Build();

			var connectionString = configuration.GetConnectionString("SQL");
			optionsBuilder.UseSqlServer(connectionString);

			return new OrderManagerDbContext(optionsBuilder.Options);
		}
	}
}
