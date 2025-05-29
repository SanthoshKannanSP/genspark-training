using BankingAPI.Interface;
using BankingAPI.Misc;
using BankingAPI.Models;
using BankingAPI.Models.DTOs;

namespace BankingAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly IRepository<int, Account> _accountRepository;
        private readonly CustomerMapper _customerMapper = new();
        private readonly AccountMapper _accountMapper = new();

        public CustomerService(
            IRepository<int, Customer> customerRepository,
            IRepository<int, Account> accountRepository
        )
        {
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }
        public async Task<Customer> AddCustomer(AddCustomerRequestDTO addCustomerRequestDTO)
        {
            try
            {
                var customer = _customerMapper.MapAddCustomerRequestToCustomer(addCustomerRequestDTO);
                customer = await _customerRepository.Add(customer);
                if (customer == null)
                    throw new Exception("Customer could not be created");
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Account> CloseAccount(int accountId)
        {
            try
            {
                var account = await _accountRepository.Get(accountId);
                if (account == null)
                    throw new Exception("Account not found");
                if (account.Status != "Active")
                    throw new Exception("The account is not active");
                account.Status = "Closed";
                account = await _accountRepository.Update(account.Id, account);
                if (account == null)
                    throw new Exception("Account could not be closed");
                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Account> CreateAccount(CreateAccountRequestDTO createAccountRequestDTO)
        {
            try
            {
                var account = _accountMapper.MapCreateAccountRequestToAccount(createAccountRequestDTO);
                account = await _accountRepository.Add(account);
                if (account == null)
                    throw new Exception("Account could not be created");
                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Account> DeactivateAccount(int accountId)
        {
            try
            {
                var account = await _accountRepository.Get(accountId);
                if (account == null)
                    throw new Exception("Account not found");
                if (account.Status != "Active")
                    throw new Exception("The account is not active");
                account.Status = "Deactivated";
                account = await _accountRepository.Update(account.Id, account);
                if (account == null)
                    throw new Exception("Account could not be deactivated");
                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<Customer>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetAll();
                return customers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}