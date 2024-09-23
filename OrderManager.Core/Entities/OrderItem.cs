namespace OrderManager.Core.Entities
{
	public class OrderItem
	{
		public OrderItem(string name, int quantity, decimal price, int orderId)
		{
			Name = name;
			Quantity = quantity;
			Price = price;
			OrderId = orderId;
		}

		public int Id { get; private set; }
		public string Name { get; private set; }
		public int Quantity { get; private set; }
		public decimal Price { get; private set; }
		public int OrderId { get; private set; }

		public Order Order { get; set; }
	}
}
