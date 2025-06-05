using Notify.Contexts;
using Notify.Interfaces;

namespace Notify.Repositories;

public  abstract class AbstractRepository<K, T> : IRepository<K, T> where T:class
    {
        protected readonly NotifyDbContext _notifyDbContext;

        public AbstractRepository(NotifyDbContext notifyDbContext)
        {
            _notifyDbContext = notifyDbContext;
        }
        public async Task<T> Add(T item)
        {
            _notifyDbContext.Add(item);
            await _notifyDbContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> Delete(K key)
        {
            var item = await Get(key);
            if (item != null)
            {
                _notifyDbContext.Remove(item);
                await _notifyDbContext.SaveChangesAsync();
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
                _notifyDbContext.Entry(myItem).CurrentValues.SetValues(item);
                await _notifyDbContext.SaveChangesAsync();
                return item;
            }
            throw new Exception("No such item found for updation");
        }
    }