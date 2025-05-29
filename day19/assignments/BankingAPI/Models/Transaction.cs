using BankingAPI.Models;

public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; }
    public DateTime Timestamp { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; }
}