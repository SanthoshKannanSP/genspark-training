using Backend.Contexts;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class ProductRepository : AbstractRepository<int, Product>
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IQueryable<Product>> GetAllAsync()
    {
        return _appDbContext.Products
            .AsQueryable();
    }

    public override async Task<Product> GetByIdAsync(int key)
    {
        return await _appDbContext.Products.FirstOrDefaultAsync(n => n.ProductId == key);
    }
}