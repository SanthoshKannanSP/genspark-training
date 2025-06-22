using AttendanceApi.Contexts;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class UserRepository : AbstractRepository<string, User>
{
    public UserRepository(AttendanceContext context):base(context)
    {
        
    }
    public override async Task<User> Get(string key)
    {
        return await _attendenceContent.Users.SingleOrDefaultAsync(t => t.Username == key);
    }

    public override async Task<IQueryable<User>> GetAll()
    {
        return _attendenceContent.Users.AsQueryable();
    }
        
}