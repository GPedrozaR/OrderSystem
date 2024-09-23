using OrderManager.Core.Entities;

namespace OrderManager.Core.Repositories
{
	public interface IOrderRepository : IRepository<Order>
	{
		Task<IEnumerable<Order>> GetAllByCustomerIdAsync(int id, int pageNumber = 1, int pageSize = 10);
	}
}
