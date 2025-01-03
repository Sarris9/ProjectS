using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectS.Data;
using ProjectS.Models;
using ProjectS.Models.Entities;

namespace ProjectS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ProjectDbContext dbContext;

        public CustomerController(ProjectDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var allCustomers = dbContext.Customers.ToList();
            return Ok(allCustomers);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = dbContext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound("Invalid Customer");
            }
            return Ok("customer found");
        }

        [HttpPost]
        public IActionResult AddCustomer(AddCustomerDto addCustomerDto)
        {
            var customerEntity = new Customer()
            {
                Name = addCustomerDto.Name,
                LastName = addCustomerDto.LastName,
                Email = addCustomerDto.Email,
                Password = addCustomerDto.Password,
                Phone = addCustomerDto.Phone
            };

            dbContext.Customers.Add(customerEntity);
            dbContext.SaveChanges();
            return Ok(customerEntity);
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customer =dbContext.Customers.Find(id);
            if (customer is null)
            {
                return NotFound("Invalid Customer");
            }
            customer.Name = updateCustomerDto.Name;
            customer.LastName = updateCustomerDto.LastName;
            customer.Email = updateCustomerDto.Email;
            customer.Password = updateCustomerDto.Password;
            customer.Phone = updateCustomerDto.Phone;
            dbContext.SaveChanges();
            return Ok("customer updated");
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = dbContext.Customers.Find(id);
            if (customer is null)
            {
                return NotFound("Invalid Customer");
            }
            dbContext.Customers.Remove(customer);
            dbContext.SaveChanges();
            return Ok("customer removed");
        }
    }
}
