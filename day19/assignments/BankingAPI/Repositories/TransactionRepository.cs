using BankingAPI.Context;
using BankingAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Models
{
    public class TransactionRepository : AbstractRepository<int, Transaction>
    {
        public TransactionRepository(BankDbContext bankDbContext) : base(bankDbContext)
        {
            
        }

        public override async Task<Transaction> Get(int id)
        {
            var transaction = await _bankDbContext.Transactions.SingleOrDefaultAsync(tra => tra.Id == id);
            return transaction ?? throw new Exception("No Transaction with the given Id");
        }

        public override async Task<IEnumerable<Transaction>> GetAll()
        {
            var transactions = _bankDbContext.Transactions;
            if (transactions.Count() == 0)
                throw new Exception("No Transaction in Database");
            return await transactions.ToListAsync();
        }
    }
}