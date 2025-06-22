using AttendanceApi.Contexts;
using AttendanceApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public abstract class AbstractRepository<K, T> : IRepository<K, T> where T : class
{
    protected readonly AttendanceContext _attendenceContent;
    public AbstractRepository(AttendanceContext attendanceContext)
    {
        _attendenceContent = attendanceContext;
    }
    public async Task<T> Add(T item)
    {
        _attendenceContent.Add(item);
        await _attendenceContent.SaveChangesAsync();
        return item;
    }

    public async Task<T> Delete(K key)
    {
        var item = await Get(key);
        if (item == null)
            throw new Exception("No such item found for deleting");

        _attendenceContent.Remove(item);
        await _attendenceContent.SaveChangesAsync();
        return item;
    }

    public abstract Task<T> Get(K key);

    public abstract Task<IQueryable<T>> GetAll();

    public async Task<T> Update(K key, T item)
    {
        var oldItem = await Get(key);
        if (oldItem == null)
            throw new Exception("No such item found for updation");

        _attendenceContent.Entry(oldItem).CurrentValues.SetValues(item);
        await _attendenceContent.SaveChangesAsync();
        return item;
    }
}