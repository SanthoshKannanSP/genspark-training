using BankingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Context
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(acc => acc.Id);
            modelBuilder.Entity<Account>().HasOne(acc => acc.Customer)
                                        .WithMany(cus => cus.Accounts)
                                        .HasForeignKey(acc => acc.CustomerId);

            modelBuilder.Entity<Transaction>().HasKey(tra => tra.Id);
            modelBuilder.Entity<Transaction>().HasOne(tra => tra.Account)
                                        .WithMany(acc => acc.Transactions)
                                        .HasForeignKey(tra => tra.AccountId);
        }
    }
}