using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace OrderPublisherTest
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			var clientCode = new Random().Next(1, 15);
			var orderCode = new Random().Next(1, 2000);

			var messages = 0;
			while (messages < 100)

			{
				var factory = new ConnectionFactory()
				{
					HostName = "localhost",
					UserName = "guest",
					Password = "guest"
				};

				using var connection = factory.CreateConnection();
				using var channel = connection.CreateModel();

				channel.QueueDeclare(queue: "orders",
									 durable: false,
									 exclusive: false,
									 autoDelete: false,
									 arguments: null);
				orderCode++;
				var pedido = new
				{
					codigoPedido = orderCode,
					codigoCliente = clientCode,
					itens = new[]
						{
					new { produto = "lápis", quantidade = 100, preco = 1.10 },
					new { produto = "caderno", quantidade = 10, preco = 1.00 }
				}
				};

				var message = JsonConvert.SerializeObject(pedido);
				var body = Encoding.UTF8.GetBytes(message);

				channel.BasicPublish(exchange: "",
									 routingKey: "orders",
									 basicProperties: null,
									 body: body);

				Console.WriteLine("Mensagem enviada: {0}", message);
				messages++;
			}

		}
	}
}
