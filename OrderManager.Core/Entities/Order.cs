namespace OrderManager.Core.Entities
{
	public class Order
	{
		public Order(int customerId)
		{
			CustomerId = customerId;
			Items = [];
		}

		public int Id { get; private set; }
		public int CustomerId { get; private set; }
		public List<OrderItem> Items { get; private set; }

		public decimal TotalValue => CalculateTotalValue();
		public Customer Customer { get; private set; }

		private decimal CalculateTotalValue() => Items.Sum(item => item.Quantity * item.Price);
	}
}
