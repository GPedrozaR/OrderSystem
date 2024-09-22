using Newtonsoft.Json;

namespace OrderManager.Application.Commands.Order.DTOs
{
	public class ItemDto
	{
		[JsonProperty("produto")]
		public string ProductName { get; set; }

		[JsonProperty("quantidade")]
		public int Quantity { get; set; }

		[JsonProperty("preco")]
		public decimal Price { get; set; }
	}
}
