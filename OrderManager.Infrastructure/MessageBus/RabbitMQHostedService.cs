using Microsoft.Extensions.Hosting;

namespace OrderManager.Infrastructure.MessageBus
{
	public class RabbitMQHostedService : BackgroundService
	{
		private readonly RabbitMqConsumer _consumer;
		public RabbitMQHostedService(RabbitMqConsumer rabbitMqConsumer)
		{
			_consumer = rabbitMqConsumer;
		}
		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			Task.Run(() =>
			{
				_consumer.Consume(RabbitMqHelper.OrderQueueName);
			}, stoppingToken);

			return Task.CompletedTask;
		}
	}
}
