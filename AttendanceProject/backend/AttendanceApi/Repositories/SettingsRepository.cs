using AttendanceApi.Contexts;
using AttendanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceApi.Repositories;

public class SettingsRepository : AbstractRepository<string, Settings>
{
    public SettingsRepository(AttendanceContext context):base(context)
    {
        
    }
    public override async Task<Settings> Get(string key)
    {
        return await _attendenceContent.Settings.SingleOrDefaultAsync(s => s.Username == key);
    }

    public override async Task<IQueryable<Settings>> GetAll()
    {
        return _attendenceContent.Settings.AsQueryable();
    }
        
}