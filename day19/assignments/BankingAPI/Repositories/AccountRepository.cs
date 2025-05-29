using BankingAPI.Context;
using BankingAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Models
{
    public class AccountRepository : AbstractRepository<int, Account>
    {
        public AccountRepository(BankDbContext bankDbContext) : base(bankDbContext)
        {
            
        }

        public override async Task<Account> Get(int id)
        {
            var account = await _bankDbContext.Accounts.SingleOrDefaultAsync(acc => acc.Id == id);
            return account ?? throw new Exception("No Account with the given Id");
        }

        public override async Task<IEnumerable<Account>> GetAll()
        {
            var accounts = _bankDbContext.Accounts;
            if (accounts.Count() == 0)
                throw new Exception("No Account in Database");
            return await accounts.ToListAsync();
        }
    }
}