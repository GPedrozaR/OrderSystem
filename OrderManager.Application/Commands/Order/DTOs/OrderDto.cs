using Newtonsoft.Json;

namespace OrderManager.Application.Commands.Order.DTOs
{
	public class OrderDto
	{
		[JsonProperty("codigoPedido")]
		public int OrderCode { get; set; }

		[JsonProperty("codigoCliente")]
		public int CustomerCode { get; set; }

		[JsonProperty("itens")]
		public List<ItemDto>? Items { get; set; }
	}
}
