namespace OrderManager.Infrastructure.MessageBus
{
	public static class RabbitMqHelper
	{
		public const string OrderQueueName = "orders";

		public const string ConnectionName = "order-connection";
	}
}
