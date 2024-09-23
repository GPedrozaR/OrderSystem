using MediatR;
using OrderManager.Application.Commands.Orders.DTOs;

namespace OrderManager.Application.Commands.Orders.CreateOrderCommand
{
	public sealed record CreateOrderCommand : IRequest
	{
		public CreateOrderCommand(int orderCode, int customerCode)
		{
			OrderCode = orderCode;
			CustomerCode = customerCode;
		}

		public int OrderCode { get; private set; }
		public int CustomerCode { get; private set; }
		public List<ItemDto> Items { get; private set; } = [];

		public void AddItems(ItemDto item) => Items.Add(item);
	}
}
