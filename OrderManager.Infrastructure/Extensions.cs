using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.Core.Repositories;
using OrderManager.Infrastructure.MessageBus;
using OrderManager.Infrastructure.Persistence.Repositories;
using RabbitMQ.Client;

namespace OrderManager.Infrastructure
{
	public static class Extensions
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services
				.AddDbContext(configuration)
				.AddDependencyInjection()
				.AddMessageBus(configuration)
				.AddHostedService<RabbitMqHostedService>();

			return services;
		}

		private static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
		{
			var rabbitMqConfig = configuration.GetSection("RabbitMQ");

			var connectionFactory = new ConnectionFactory
			{
				HostName = rabbitMqConfig["HostName"],
				UserName = rabbitMqConfig["UserName"],
				Password = rabbitMqConfig["Password"]
			};

			var connection = connectionFactory.CreateConnection(RabbitMqHelper.ConnectionName);
			services.AddSingleton(connection);
			services.AddTransient<RabbitMqConsumer>();

			return services;
		}

		private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<OrderManagerDbContext>(
				options => options.UseSqlServer(configuration.GetConnectionString("SQL")));

			return services;
		}

		private static IServiceCollection AddDependencyInjection(this IServiceCollection services)
		{
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<IOrderItemRepository, OrderItemRepository>();
			services.AddScoped<ICustomerRepository, CustomerRepository>();

			return services;
		}
	}
}
