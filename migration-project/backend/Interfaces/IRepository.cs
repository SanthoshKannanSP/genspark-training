namespace Backend.Interfaces;

public interface IRepository<K, T> where T : class
{
    public Task<T> AddAsync(T item);
    public Task<T> GetByIdAsync(K key);
    public Task<IQueryable<T>> GetAllAsync();
    public Task<T> UpdateAsync(K key, T item);
    public Task<T> DeleteAsync(K key);
}