using Backend.Contexts;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class UserRepository : AbstractRepository<int, User>
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<User> GetByIdAsync(int key)
    {
        return await _appDbContext.Users.FirstOrDefaultAsync(n => n.UserId == key);
    }

    public override async Task<IQueryable<User>> GetAllAsync()
    {
        return _appDbContext.Users
            .AsQueryable();
    }
}
