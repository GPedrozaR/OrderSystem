using OrderManager.Core.Entities;
using OrderManager.Core.Repositories;

namespace OrderManager.Infrastructure.Persistence.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		public async Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Customer>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<Customer> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task InsertAsync(Customer entity)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateAsync(int id, Customer entity)
		{
			throw new NotImplementedException();
		}
	}
}
