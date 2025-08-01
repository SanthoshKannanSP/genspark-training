using Backend.Contexts;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class OrderRepository : AbstractRepository<int, Order>
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<Order> GetByIdAsync(int key)
    {
        return await _appDbContext.Orders.FirstOrDefaultAsync(n => n.OrderID == key);
    }

    public override async Task<IQueryable<Order>> GetAllAsync()
    {
        return _appDbContext.Orders
            .AsQueryable();
    }
}
