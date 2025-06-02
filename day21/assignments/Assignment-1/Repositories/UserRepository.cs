using assignment_1.Contexts;
using assignment_1.Models;
using Microsoft.EntityFrameworkCore;

namespace assignment_1.Repositories
{
    public class UserRepository : AbstractRepository<int, User>
    {
        public UserRepository(ClinicContext context):base(context)
        {
            
        }
        public override async Task<User> Get(int key)
        {
            return await _clinicContext.Users.SingleOrDefaultAsync(u => u.UserId == key);
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _clinicContext.Users.ToListAsync();
        }
            
    }
}