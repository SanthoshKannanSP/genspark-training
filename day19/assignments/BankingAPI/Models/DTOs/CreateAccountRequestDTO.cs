namespace BankingAPI.Models.DTOs
{
    public class CreateAccountRequestDTO
    {
        public string Type { get; set; }
        public int CustomerId { get; set; }
    }
}