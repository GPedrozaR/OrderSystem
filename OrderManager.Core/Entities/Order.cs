namespace OrderManager.Core.Entities
{
	public class Order
	{
		public Order(int customerId, IEnumerable<OrderItem> items)
		{
			CustomerId = customerId;
			Items = items;
		}

		public int Id { get; set; }
		public int CustomerId { get; private set; }
		public IEnumerable<OrderItem> Items { get; private set; }

		public Customer Customer { get; private set; }

		public void UpdateItems(IEnumerable<OrderItem> items) => Items = items;
	}
}
