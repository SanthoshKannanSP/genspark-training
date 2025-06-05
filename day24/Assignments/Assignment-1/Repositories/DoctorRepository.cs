using assignment_1.Contexts;
using assignment_1.Models;
using Microsoft.EntityFrameworkCore;

namespace assignment_1.Repositories
{
    public class DoctorRepository : AbstractRepository<int, Doctor>
    {
        public DoctorRepository(ClinicContext context):base(context)
        {
            
        }
        public override async Task<Doctor> Get(int key)
        {
            return await _clinicContext.Doctors.SingleOrDefaultAsync(u => u.Id == key);
        }

        public override async Task<IEnumerable<Doctor>> GetAll()
        {
            return await _clinicContext.Doctors.ToListAsync();
        }
            
    }
}