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
			return await _context.Orders.AsNoTracking().ToListAsync();
		}

		public async Task<Order> GetByIdAsync(int id)
		{
			return await _context.Orders
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task InsertAsync(Order entity)
		{
			await _context.Orders.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(int id, Order entity)
		{
			var existingOrder = await _context.Orders.FindAsync(id);

			if (existingOrder != null)
			{
				existingOrder.UpdateItems(entity.Items);
				_context.Orders.Update(existingOrder);

				await _context.SaveChangesAsync();
			}
		}
	}
}
