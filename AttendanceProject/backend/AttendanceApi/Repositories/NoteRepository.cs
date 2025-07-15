using AttendanceApi.Contexts;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class NoteRepository : AbstractRepository<int, Notes>
{
    public NoteRepository(AttendanceContext context):base(context)
    {
        
    }
    public override async Task<Notes> Get(int key)
    {
        return await _attendenceContent.Notes.SingleOrDefaultAsync(s => s.NoteId == key);
    }

    public override async Task<IQueryable<Notes>> GetAll()
    {
        return _attendenceContent.Notes.AsQueryable();
    }
}