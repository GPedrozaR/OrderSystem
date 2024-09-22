using Microsoft.AspNetCore.Mvc;

namespace OrderManager.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrdersController : ControllerBase
	{
		[HttpGet("{id}/total-value")]
		public async Task<IActionResult> GetOrderTotalValue(int id)
		{
			return Ok();
		}

		[HttpGet("customer/{customerId}/order-count")]
		public async Task<IActionResult> GetOrderCountByCustomer(int customerId)
		{
			return Ok();
		}

		[HttpGet("customer/{customerId}/order-list")]
		public async Task<IActionResult> GetOrdersByCustomer(int customerId)
		{
			return Ok();
		}
	}
}
