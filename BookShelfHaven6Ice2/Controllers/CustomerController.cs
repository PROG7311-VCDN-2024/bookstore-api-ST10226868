using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShelfHaven6Ice2.Models;

namespace BookShelfHaven6Ice2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly BookShelfHavenContext _context;

        public CustomerController(BookShelfHavenContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public IActionResult GetCustomers()
        {
            return Ok(_context.Customers);
        }

        // GET: api/Customer/5
        [HttpGet("{username}")]
        public IActionResult GetCustomer(string username)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Username == username);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public IActionResult PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCustomer), new { username = customer.Username }, customer);
        }

        // PUT: api/Customer/5
        [HttpPut("{username}")]
        public IActionResult PutCustomer(string username, Customer customer)
        {
            if (username != customer.Username)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Customer/5
        [HttpDelete("{username}")]
        public IActionResult DeleteCustomer(string username)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Username == username);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return NoContent();
        }

        private bool CustomerExists(string username)
        {
            return _context.Customers.Any(c => c.Username == username);
        }
    }
}
