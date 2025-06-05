using assignment_1.Contexts;
using assignment_1.Models;
using Microsoft.EntityFrameworkCore;

namespace assignment_1.Repositories
{
    public class AppointmentRepository : AbstractRepository<int, Appointment>
    {
        public AppointmentRepository(ClinicContext context):base(context)
        {
            
        }
        public override async Task<Appointment> Get(int key)
        {
            return await _clinicContext.Appointments.SingleOrDefaultAsync(u => u.AppointmentNumber == key);
        }

        public override async Task<IEnumerable<Appointment>> GetAll()
        {
            return await _clinicContext.Appointments.ToListAsync();
        }
            
    }
}