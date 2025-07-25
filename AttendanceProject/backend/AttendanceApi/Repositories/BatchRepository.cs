using AttendanceApi.Contexts;
using AttendanceApi.Interfaces;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class BatchRepository : AbstractRepository<int, Batch>
{
    public BatchRepository(AttendanceContext context) : base(context) { }

    public override async Task<Batch> Get(int id)
    {
        return await _attendenceContent.Batches
            .Include(b => b.Students)
            .FirstOrDefaultAsync(b => b.BatchId == id);
    }

    public override async Task<IQueryable<Batch>> GetAll()
    {
        return await Task.FromResult(
            _attendenceContent.Batches
                .Include(b => b.Students)
                .AsQueryable()
        );
    }
}
