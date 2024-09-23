using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OrderManager.Application.Commands.Orders.CreateOrderCommand;
using OrderManager.Application.Commands.Orders.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace OrderManager.Infrastructure.MessageBus
{
	public class RabbitMqConsumer : IDisposable
	{
		private readonly IConnection _connection;
		private readonly IMediator _mediator;
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly IModel _channel;

		public RabbitMqConsumer(IConnection connection, IMediator mediator, IServiceScopeFactory serviceScopeFactory)
		{
			_connection = connection;
			_mediator = mediator;
			_channel = _connection.CreateModel();
			_serviceScopeFactory = serviceScopeFactory;
		}

		public void Consume(string queueName)
		{
			_channel.QueueDeclare(queue: queueName,
								 durable: false,
								 exclusive: false,
								 autoDelete: false,
								 arguments: null);

			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += async (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);

				try
				{
					var order = JsonConvert.DeserializeObject<OrderDto>(message) ??
								throw new ArgumentException("Invalid json", message);

					await ProcessMessage(order);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
				}
			};

			_channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
		}

		private async Task ProcessMessage(OrderDto order)
		{
			using var scope = _serviceScopeFactory.CreateScope();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

			var command = new CreateOrderCommand(order.OrderCode, order.CustomerCode);
			if (order.Items != null)
			{
				foreach (var item in order.Items)
					command.AddItems(new ItemDto(item.Product, item.Quantity, item.Price));
			}

			await mediator.Send(command);
		}

		public void Dispose()
		{
			_channel?.Close();
			_connection?.Close();
		}
	}
}
