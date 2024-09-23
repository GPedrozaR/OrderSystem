using Microsoft.EntityFrameworkCore;
using OrderManager.Core.Entities;
using OrderManager.Core.Repositories;

namespace OrderManager.Infrastructure.Persistence.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly OrderManagerDbContext _context;
		public OrderRepository(OrderManagerDbContext context)
		{
			_context = context;
		}

		public async Task DeleteAsync(int id)
		{
			var order = await GetByIdAsync(id);

			if (order != null)
			{
				_context.Orders.Remove(order);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Order>> GetAllAsync()
		{
			return await _context
				.Orders
				.Include(x => x.Items)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<IEnumerable<Order>> GetAllByCustomerIdAsync(int id, int pageNumber = 1, int pageSize = 10)
		{
			return await _context
				.Orders
				.Where(x => x.CustomerId == id)
				.Include(x => x.Items)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<Order> GetByIdAsync(int id)
		{
			return await _context.Orders
				.Include(x => x.Items)
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task InsertAsync(Order entity)
		{
			await _context.Orders.AddAsync(entity);
			await _context.SaveChangesAsync();
		}
	}
}
