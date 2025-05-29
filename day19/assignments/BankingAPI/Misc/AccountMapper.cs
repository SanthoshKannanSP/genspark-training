using BankingAPI.Models;
using BankingAPI.Models.DTOs;

namespace BankingAPI.Misc
{
    public class AccountMapper
    {
        public Account MapCreateAccountRequestToAccount(CreateAccountRequestDTO createAccountRequestDTO)
        {
            Account account = new()
            {
                AccountNumber = "ss",
                Balance = 0,
                Type = createAccountRequestDTO.Type,
                CustomerId = createAccountRequestDTO.CustomerId,
                Status = "Active"
            };
            return account;
        }
    }
}