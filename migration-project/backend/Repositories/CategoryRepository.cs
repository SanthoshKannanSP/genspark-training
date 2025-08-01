using Backend.Contexts;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class CategoryRepository : AbstractRepository<int, Category>
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<Category> GetByIdAsync(int key)
    {
        return await _appDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == key);
    }

    public override async Task<IQueryable<Category>> GetAllAsync()
    {
        return _appDbContext.Categories
            .AsQueryable();
    }
}