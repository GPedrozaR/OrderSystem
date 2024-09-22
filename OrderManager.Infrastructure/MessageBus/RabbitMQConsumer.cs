using MediatR;
using Newtonsoft.Json;
using OrderManager.Application.Commands.Order.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace OrderManager.Infrastructure.MessageBus
{
	public class RabbitMqConsumer : IDisposable
	{
		private readonly IConnection _connection;
		private readonly IMediator _mediator;
		private readonly IModel _channel;

		public RabbitMqConsumer(IConnection connection, IMediator mediator)
		{
			_connection = connection;
			_mediator = mediator;
			_channel = _connection.CreateModel();
		}

		public void Consume(string queueName)
		{
			_channel.QueueDeclare(queue: queueName,
								 durable: false,
								 exclusive: false,
								 autoDelete: false,
								 arguments: null);

			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);

				var order = JsonConvert.DeserializeObject<OrderDto>(message);
				ProcessMessage(order, message);
			};

			_channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
		}

		private void ProcessMessage(OrderDto order, string message)
		{
			Console.WriteLine($"Pedido recebido: {order.OrderCode}");
		}

		public void Dispose()
		{
			_channel?.Close();
			_connection?.Close();
		}
	}
}
