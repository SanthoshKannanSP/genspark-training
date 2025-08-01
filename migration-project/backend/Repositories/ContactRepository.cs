using Backend.Contexts;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class ContactUsRepository : AbstractRepository<int, ContactUs>
{
    public ContactUsRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<ContactUs> GetByIdAsync(int key)
    {
        return await _appDbContext.ContactUs.FirstOrDefaultAsync(c => c.Id == key);
    }

    public override async Task<IQueryable<ContactUs>> GetAllAsync()
    {
        return _appDbContext.ContactUs
            .AsQueryable();
    }
}