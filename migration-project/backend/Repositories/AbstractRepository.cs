using Backend.Contexts;
using Backend.Interfaces;

namespace Backend.Repositories;

public abstract class AbstractRepository<K, T> : IRepository<K, T> where T : class
{
    protected readonly AppDbContext _appDbContext;
    public AbstractRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<T> AddAsync(T item)
    {
        _appDbContext.Add(item);
        await _appDbContext.SaveChangesAsync();
        return item;
    }

    public async Task<T> DeleteAsync(K key)
    {
        var item = await GetByIdAsync(key);
        if (item == null)
            throw new Exception("No such item found for deleting");

        _appDbContext.Remove(item);
        await _appDbContext.SaveChangesAsync();
        return item;
    }

    public abstract Task<T> GetByIdAsync(K key);

    public abstract Task<IQueryable<T>> GetAllAsync();

    public async Task<T> UpdateAsync(K key, T item)
    {
        var oldItem = await GetByIdAsync(key);
        if (oldItem == null)
            throw new Exception("No such item found for updation");

        _appDbContext.Entry(oldItem).CurrentValues.SetValues(item);
        await _appDbContext.SaveChangesAsync();
        return item;
    }
}