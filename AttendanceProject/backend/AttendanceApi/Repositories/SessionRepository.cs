using AttendanceApi.Contexts;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class SessionRepository : AbstractRepository<int, Session>
{
    public SessionRepository(AttendanceContext context):base(context)
    {
        
    }
    public override async Task<Session> Get(int key)
    {
        return await _attendenceContent.Sessions.Include(s => s.MadeBy).SingleOrDefaultAsync(s => s.SessionId == key);
    }

    public override async Task<IQueryable<Session>> GetAll()
    {
        return _attendenceContent.Sessions.AsQueryable();
    }
        
}