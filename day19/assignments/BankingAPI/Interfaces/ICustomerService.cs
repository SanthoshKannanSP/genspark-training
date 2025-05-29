using BankingAPI.Models;
using BankingAPI.Models.DTOs;

namespace BankingAPI.Interface
{
    public interface ICustomerService
    {
        public Task<Customer> AddCustomer(AddCustomerRequestDTO addCustomerRequestDTO);
        public Task<ICollection<Customer>> GetAllCustomers();
        public Task<Account> CreateAccount(CreateAccountRequestDTO createAccountRequestDTO);
        public Task<Account> DeactivateAccount(int accountId);
        public Task<Account> CloseAccount(int accountId);
    }
}