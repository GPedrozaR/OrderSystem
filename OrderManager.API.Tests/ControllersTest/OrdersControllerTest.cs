using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using OrderManager.Application.Commands.Orders.DTOs;
using System.Globalization;

namespace OrderManager.API.Tests.ControllersTest
{
	public class OrdersControllerTests : IClassFixture<WebApplicationFactory<Program>>
	{

		private readonly HttpClient _httpClient;
		public OrdersControllerTests(WebApplicationFactory<Program> factory)
		{
			_httpClient = factory.CreateClient();
		}

		[Fact]
		public async Task GetTotalValue_ShouldReturnCorrectValue()
		{
			var expectedValue = 120m;

			var orderId = 440;

			var response = await _httpClient.GetAsync($"/api/Orders/{orderId}/total-value");
			response.EnsureSuccessStatusCode();

			var responseStr = await response.Content.ReadAsStringAsync();

			if (!decimal.TryParse(responseStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var actualValue))
				Assert.Fail($"A resposta não pôde ser convertida para decimal: {responseStr}");

			Assert.Equal(expectedValue, actualValue);
		}

		[Fact]
		public async Task GetOrderCount_ShouldReturnCorrectCount()
		{
			var expectedCount = 82;

			var customerId = 1;

			var response = await _httpClient.GetAsync($"/api/Orders/customer/{customerId}/order-count");
			response.EnsureSuccessStatusCode();

			var responseCount = await response.Content.ReadAsStringAsync();

			Assert.Equal(expectedCount.ToString(), responseCount);
		}

		[Fact]
		public async Task GetOrderListWithoutPageSize_ShouldReturn10Orders()
		{
			var customerId = 1;

			var response = await _httpClient.GetAsync($"/api/Orders/customer/{customerId}/order-list");
			response.EnsureSuccessStatusCode();

			var actualOrderList = await response.Content.ReadAsStringAsync();

			var orders = JsonConvert.DeserializeObject<List<OrderDto>>(actualOrderList);

			Assert.NotNull(orders);
			Assert.Equal(10, orders.Count);
		}

		[Fact]
		public async Task GetOrderListWithPageSize_ShouldReturnPageSizeOrders()
		{
			var customerId = 1;

			var response = await _httpClient.GetAsync($"/api/Orders/customer/{customerId}/order-list?pageSize=82");
			response.EnsureSuccessStatusCode();

			var actualOrderList = await response.Content.ReadAsStringAsync();

			var orders = JsonConvert.DeserializeObject<List<OrderDto>>(actualOrderList);

			Assert.NotNull(orders);
			Assert.Equal(82, orders.Count);
		}
	}
}
