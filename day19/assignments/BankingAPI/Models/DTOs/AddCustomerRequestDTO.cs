namespace BankingAPI.Models.DTOs
{
    public class AddCustomerRequestDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}