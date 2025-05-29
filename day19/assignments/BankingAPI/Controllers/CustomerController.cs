using BankingAPI.Interface;
using BankingAPI.Models;
using BankingAPI.Models.DTOs;
using BankingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(AddCustomerRequestDTO addCustomerRequestDTO)
        {
            var customer = await _customerService.AddCustomer(addCustomerRequestDTO);
            return Created("", customer);
        }
    }
}