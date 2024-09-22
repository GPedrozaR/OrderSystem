using OrderManager.Core.Entities;
using OrderManager.Core.Repositories;

namespace OrderManager.Infrastructure.Persistence.Repositories
{
	public class OrderItemRepository : IOrderItemRepository
	{
		public async Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<OrderItem>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<OrderItem> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task InsertAsync(OrderItem entity)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateAsync(int id, OrderItem entity)
		{
			throw new NotImplementedException();
		}
	}
}
