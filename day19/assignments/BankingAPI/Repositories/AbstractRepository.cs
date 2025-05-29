using BankingAPI.Context;
using BankingAPI.Interface;

namespace BankingAPI.Repositories
{
    public abstract class AbstractRepository<K, T> : IRepository<K, T> where T : class
    {
        protected readonly BankDbContext _bankDbContext;
        public AbstractRepository(BankDbContext bankDbContext)
        {
            _bankDbContext = bankDbContext;
        }

        public async Task<T> Add(T item)
        {
            _bankDbContext.Add(item);
            await _bankDbContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> Delete(K key)
        {
            var item = await Get(key);
            if (item != null)
            {
                _bankDbContext.Remove(item);
                await _bankDbContext.SaveChangesAsync();
                return item;
            }
            throw new Exception("No such item found for deleting");
        }

        public abstract Task<T> Get(K key);

        public abstract Task<IEnumerable<T>> GetAll();

        public async Task<T> Update(K key, T item)
        {
            var myItem = await Get(key);
            if (myItem != null)
            {
                _bankDbContext.Entry(myItem).CurrentValues.SetValues(item);
                await _bankDbContext.SaveChangesAsync();
                return item;
            }
            throw new Exception("No such item found for updation");
        }
    }
}