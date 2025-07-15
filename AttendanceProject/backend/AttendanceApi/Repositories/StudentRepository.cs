using AttendanceApi.Contexts;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class StudentRepository : AbstractRepository<int, Student>
{
    public StudentRepository(AttendanceContext context):base(context)
    {
        
    }
    public override async Task<Student> Get(int key)
    {
        return await _attendenceContent.Students.SingleOrDefaultAsync(t => t.StudentId == key);
    }

    public override async Task<IQueryable<Student>> GetAll()
    {
        return _attendenceContent.Students.AsQueryable();
    }
        
}