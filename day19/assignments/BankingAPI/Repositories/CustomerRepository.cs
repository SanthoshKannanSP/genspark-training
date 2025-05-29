using BankingAPI.Context;
using BankingAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Models
{
    public class CustomerRepository : AbstractRepository<int, Customer>
    {
        public CustomerRepository(BankDbContext bankDbContext) : base(bankDbContext)
        {
            
        }

        public override async Task<Customer> Get(int id)
        {
            var customer = await _bankDbContext.Customers.SingleOrDefaultAsync(cus => cus.Id == id);
            return customer ?? throw new Exception("No Customer with the given Id");
        }

        public override async Task<IEnumerable<Customer>> GetAll()
        {
            var customers = _bankDbContext.Customers;
            if (customers.Count() == 0)
                throw new Exception("No Customer in Database");
            return await customers.ToListAsync();
        }
    }
}