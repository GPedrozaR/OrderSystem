using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.Infrastructure.MessageBus;
using RabbitMQ.Client;

namespace OrderManager.Infrastructure
{
	public static class Extensions
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services
				.AddMessageBus(configuration)
				.AddDbContext(configuration)
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
			var sqlConfig = configuration.GetSection("SQL");

			services.AddDbContext<OrderManagerDbContext>(
				options =>
					options.UseSqlServer(sqlConfig["ConnectionString"]
				));

			return services;
		}
	}
}
