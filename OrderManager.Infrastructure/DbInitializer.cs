using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OrderManager.Infrastructure
{
	public static class DbInitializer
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using var context = new OrderManagerDbContext(serviceProvider.GetRequiredService<DbContextOptions<OrderManagerDbContext>>());
			context.Database.Migrate();
		}
	}
}
