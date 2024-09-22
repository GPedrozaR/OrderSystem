using MediatR;
using OrderManager.Application.Commands.Order.DTOs;

namespace OrderManager.Application.Commands.Order.CreateOrderCommand
{
	public sealed record CreateOrderCommand : IRequest
	{
		public int OrderCode { get; set; }
		public int CustomerCode { get; set; }
		public List<ItemDto> Items { get; set; }
	}
}
