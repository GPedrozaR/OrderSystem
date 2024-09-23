using Microsoft.EntityFrameworkCore;
using OrderManager.Core.Entities;
using OrderManager.Core.Repositories;

namespace OrderManager.Infrastructure.Persistence.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{

		private readonly OrderManagerDbContext _context;
		public CustomerRepository(OrderManagerDbContext context)
		{
			_context = context;
		}

		public async Task DeleteAsync(int id)
		{
			var customer = await GetByIdAsync(id);

			if (customer != null)
			{
				_context.Customers.Remove(customer);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Customer>> GetAllAsync()
		{
			return await _context.Customers
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<Customer> GetByIdAsync(int id)
		{
			return await _context.Customers
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task InsertAsync(Customer entity)
		{
			await _context.Customers.AddAsync(entity);
			await _context.SaveChangesAsync();
		}
	}
}
