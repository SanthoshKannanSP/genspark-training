using AttendanceApi.Contexts;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class SessionAttendanceRepository : AbstractRepository<int, SessionAttendance>
{
    public SessionAttendanceRepository(AttendanceContext context):base(context)
    {
        
    }
    public override async Task<SessionAttendance> Get(int key)
    {
        return await _attendenceContent.SessionAttendances.Include(s=> s.Student).Include(s=> s.Session).SingleOrDefaultAsync(t => t.SessionAttendanceId == key);
    }

    public override async Task<IEnumerable<SessionAttendance>> GetAll()
    {
        return await _attendenceContent.SessionAttendances.Include(s=>s.Session).ToListAsync();
    }
        
}