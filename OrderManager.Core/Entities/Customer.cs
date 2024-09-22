namespace OrderManager.Core.Entities
{
	public class Customer
	{
		public Customer(string name)
		{
			Name = name;
		}

		public int Id { get; private set; }
		public string Name { get; private set; }

		public List<Order> Orders { get; private set; }
	}
}
