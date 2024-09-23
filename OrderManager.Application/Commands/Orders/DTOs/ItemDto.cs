using Newtonsoft.Json;

namespace OrderManager.Application.Commands.Orders.DTOs
{
	public class ItemDto
	{
		public ItemDto(string product, int quantity, decimal price)
		{
			Product = product;
			Quantity = quantity;
			Price = price;
		}

		public ItemDto() { }

		[JsonProperty("produto")]
		public string Product { get; set; }

		[JsonProperty("quantidade")]
		public int Quantity { get; set; }

		[JsonProperty("preco")]
		public decimal Price { get; set; }
	}
}
