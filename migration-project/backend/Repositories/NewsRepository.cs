using Backend.Contexts;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class NewsRepository : AbstractRepository<int, News>
{
    public NewsRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<News> GetByIdAsync(int key)
    {
        return await _appDbContext.News.Include(n => n.User).FirstOrDefaultAsync(n => n.NewsId == key);
    }

    public override async Task<IQueryable<News>> GetAllAsync()
    {
        return _appDbContext.News.Include(n => n.User)
            .AsQueryable();
    }
}