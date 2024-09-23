using Microsoft.EntityFrameworkCore;
using OrderManager.Core.Entities;
using OrderManager.Core.Repositories;

namespace OrderManager.Infrastructure.Persistence.Repositories
{
	public class OrderItemRepository : IOrderItemRepository
	{
		private readonly OrderManagerDbContext _context;
		public OrderItemRepository(OrderManagerDbContext context)
		{
			_context = context;
		}

		public async Task DeleteAsync(int id)
		{
			var orderItem = await GetByIdAsync(id);

			if (orderItem != null)
			{
				_context.OrderItems.Remove(orderItem);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<OrderItem>> GetAllAsync()
		{
			return await _context.OrderItems
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<OrderItem> GetByIdAsync(int id)
		{
			return await _context.OrderItems
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task InsertAsync(OrderItem entity)
		{
			await _context.OrderItems.AddAsync(entity);
			await _context.SaveChangesAsync();
		}
	}
}
