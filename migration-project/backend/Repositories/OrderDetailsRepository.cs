using Backend.Contexts;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class OrderDetailsRepository : AbstractRepository<int, OrderDetail>
{
    public OrderDetailsRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<OrderDetail> GetByIdAsync(int key)
    {
        return await _appDbContext.OrderDetails.FirstOrDefaultAsync(n => n.OrderID == key);
    }

    public override async Task<IQueryable<OrderDetail>> GetAllAsync()
    {
        return _appDbContext.OrderDetails
            .AsQueryable();
    }
}
