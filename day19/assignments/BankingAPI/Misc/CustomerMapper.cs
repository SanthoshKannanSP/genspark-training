using BankingAPI.Models;
using BankingAPI.Models.DTOs;

namespace BankingAPI.Misc
{
    public class CustomerMapper
    {
        public Customer MapAddCustomerRequestToCustomer(AddCustomerRequestDTO addCustomerRequestDTO)
        {
            Customer customer = new()
            {
                FullName = addCustomerRequestDTO.FullName,
                Email = addCustomerRequestDTO.Email,
                PhoneNumber = addCustomerRequestDTO.PhoneNumber
            };
            return customer;
        }
    }
}