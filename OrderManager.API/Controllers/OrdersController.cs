using Microsoft.AspNetCore.Mvc;
using OrderManager.Application.Commands.Orders.DTOs;
using OrderManager.Core.Repositories;

namespace OrderManager.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderRepository _orderRepository;

		public OrdersController(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		[HttpGet("{id}/total-value")]
		public async Task<IActionResult> GetOrderTotalValue(int id)
		{
			var order = await _orderRepository.GetByIdAsync(id);
			return order == null 
				? NotFound() 
				: Ok(order.TotalValue);
		}

		[HttpGet("customer/{customerId}/order-count")]
		public async Task<IActionResult> GetOrderCountByCustomer(int customerId)
		{
			var orders = await _orderRepository.GetAllAsync();
			var count = orders.Count(o=> o.CustomerId == customerId);

			return orders == null 
				? NotFound() 
				: Ok(count);
		}

		[HttpGet("customer/{customerId}/order-list")]
		public async Task<IActionResult> GetOrdersByCustomer(int customerId, int pageNumber = 1, int pageSize = 10)
		{
			var orders = await _orderRepository.GetAllByCustomerIdAsync(customerId, pageNumber, pageSize);

			var ordersDto = orders.Select(order => new OrderDto
			{
				OrderCode = order.Id,
				CustomerCode = order.CustomerId,
				Items = order.Items.Select(item => new ItemDto
				{
					Product = item.Name,
					Quantity = item.Quantity,
					Price = item.Price
				}).ToList()

			}).ToList();

			return orders == null ? NotFound() : Ok(ordersDto);
		}
	}
}
