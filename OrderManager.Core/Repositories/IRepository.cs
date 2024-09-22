namespace OrderManager.Core.Repositories
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task UpdateAsync(int id, T entity);
		Task InsertAsync(T entity);
		Task DeleteAsync(int id);
	}
}
