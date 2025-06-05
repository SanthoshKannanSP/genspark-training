using assignment_1.Contexts;
using assignment_1.Models;
using Microsoft.EntityFrameworkCore;

namespace assignment_1.Repositories
{
    public class UserRepository : AbstractRepository<string, User>
    {
        public UserRepository(ClinicContext context):base(context)
        {
            
        }
        public override async Task<User> Get(string key)
        {
            return await _clinicContext.Users.Include(user => user.Doctor).SingleOrDefaultAsync(u => u.Username == key);
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _clinicContext.Users.ToListAsync();
        }
            
    }
}