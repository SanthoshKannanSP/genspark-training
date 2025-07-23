using AttendanceApi.Contexts;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class AttendanceEditRequestRepository : AbstractRepository<int, AttendanceEditRequest>
{
    public AttendanceEditRequestRepository(AttendanceContext context) : base(context)
    {
    }

    public override async Task<AttendanceEditRequest> Get(int key)
    {
        return await _attendenceContent.AttendanceEditRequests
            .Include(r => r.SessionAttendance)
            .ThenInclude(sa => sa.Student)
            .FirstOrDefaultAsync(r => r.Id == key);
    }

    public override async Task<IQueryable<AttendanceEditRequest>> GetAll()
    {
        return _attendenceContent.AttendanceEditRequests
            .Include(r => r.SessionAttendance)
            .ThenInclude(sa => sa.Student)
            .AsQueryable();
    }
}
