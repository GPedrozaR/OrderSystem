using Microsoft.Extensions.DependencyInjection;
using OrderManager.Application.Commands.Orders.CreateOrderCommand;

namespace OrderManager.Application
{
	public static class Extensions
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services
				.AddMediatR();

			return services;
		}

		private static IServiceCollection AddMediatR(this IServiceCollection services)
		{
			services.AddMediatR(op => op.RegisterServicesFromAssemblyContaining(typeof(CreateOrderCommand)));
			return services;
		}
	}
}
