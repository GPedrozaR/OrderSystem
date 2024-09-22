using Newtonsoft.Json;
using OrderManager.Core.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace OrderManager.Infrastructure.MessageBus
{
	public class RabbitMqConsumer : IDisposable
	{
		private readonly IConnection _connection;
		private readonly IModel _channel;

		public RabbitMqConsumer(IConnection connection)
		{
			_connection = connection;
			_channel = _connection.CreateModel();
		}

		public void Consume(string queueName)
		{
			_channel.QueueDeclare(queue: queueName,
								 durable: true,
								 exclusive: false,
								 autoDelete: false,
								 arguments: null);

			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);

				var order = JsonConvert.DeserializeObject<Order>(message);
				ProcessMessage(order);
			};

			_channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
		}

		private void ProcessMessage(Order order)
		{
			Console.WriteLine($"Pedido recebido: {order.Id}");
		}

		public void Dispose()
		{
			_channel?.Close();
			_connection?.Close();
		}
	}
}
