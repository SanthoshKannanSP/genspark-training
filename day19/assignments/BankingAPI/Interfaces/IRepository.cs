namespace BankingAPI.Interface
{
    public interface IRepository<K, T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(K key);
        Task<T> Add(T item);
        Task<T> Update(K key, T item);
        Task<T> Delete(K key);
    }
}
