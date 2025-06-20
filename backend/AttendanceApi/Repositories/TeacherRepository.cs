using AttendanceApi.Contexts;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class TeacherRepository : AbstractRepository<int, Teacher>
{
    public TeacherRepository(AttendanceContext context):base(context)
    {
        
    }
    public override async Task<Teacher> Get(int key)
    {
        return await _attendenceContent.Teachers.SingleOrDefaultAsync(t => t.TeacherId == key);
    }

    public override async Task<IEnumerable<Teacher>> GetAll()
    {
        return await _attendenceContent.Teachers.ToListAsync();
    }
        
}