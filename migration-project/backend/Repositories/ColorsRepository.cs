using Backend.Contexts;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class ColorsRepository : AbstractRepository<int, Color>
{
    public ColorsRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<Color> GetByIdAsync(int key)
    {
        return await _appDbContext.Colors.FirstOrDefaultAsync(c => c.ColorId == key);
    }

    public override async Task<IQueryable<Color>> GetAllAsync()
    {
        return _appDbContext.Colors
            .AsQueryable();
    }
}