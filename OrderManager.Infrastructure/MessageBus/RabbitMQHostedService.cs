using Microsoft.Extensions.Hosting;

namespace OrderManager.Infrastructure.MessageBus
{
	public class RabbitMqHostedService : BackgroundService
	{
		private readonly RabbitMqConsumer _consumer;

		public RabbitMqHostedService(RabbitMqConsumer rabbitMqConsumer)
		{
			_consumer = rabbitMqConsumer;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					_consumer.Consume(RabbitMqHelper.OrderQueueName);
					await Task.Delay(1000, stoppingToken);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Erro ao consumir mensagens: {ex.Message}");
					await Task.Delay(5000, stoppingToken); 
				}
			}
		}
	}

}
