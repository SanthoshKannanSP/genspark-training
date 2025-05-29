namespace BankingAPI.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }

        public string Type { get; set; } // Current, Savings
        public int CustomerId { get; set; }
        public string Status { get; set; } // Active, Deactivated, Closed
        public Customer Customer { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}