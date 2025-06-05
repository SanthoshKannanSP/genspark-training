using Notify.Contexts;
using Notify.Models;
using Microsoft.EntityFrameworkCore;

namespace Notify.Repositories
{
    public class UserRepository : AbstractRepository<string, User>
    {
        public UserRepository(NotifyDbContext context):base(context)
        {
            
        }
        public override async Task<User> Get(string key)
        {
            return await _notifyDbContext.Users.SingleOrDefaultAsync(u => u.Username == key);
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _notifyDbContext.Users.ToListAsync();
        }
            
    }
}