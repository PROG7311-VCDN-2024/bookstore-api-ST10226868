using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShelfHaven6Ice2.Models;

namespace BookShelfHaven6Ice2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly BookShelfHavenContext _context;

        public CheckoutController(BookShelfHavenContext context)
        {
            _context = context;
        }

        // GET: api/Checkout
        [HttpGet]
        public IActionResult GetCheckouts()
        {
            return Ok(_context.Checkouts);
        }

        // GET: api/Checkout/5
        [HttpGet("{id}")]
        public IActionResult GetCheckout(int id)
        {
            var checkout = _context.Checkouts.Find(id);

            if (checkout == null)
            {
                return NotFound();
            }

            return Ok(checkout);
        }

        // POST: api/Checkout
        [HttpPost]
        public IActionResult PostCheckout(Checkout checkout)
        {
            _context.Checkouts.Add(checkout);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCheckout), new { id = checkout.CheckoutId }, checkout);
        }

        // PUT: api/Checkout/5
        [HttpPut("{id}")]
        public IActionResult PutCheckout(int id, Checkout checkout)
        {
            if (id != checkout.CheckoutId)
            {
                return BadRequest();
            }

            _context.Entry(checkout).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Checkout/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCheckout(int id)
        {
            var checkout = _context.Checkouts.Find(id);
            if (checkout == null)
            {
                return NotFound();
            }

            _context.Checkouts.Remove(checkout);
            _context.SaveChanges();

            return NoContent();
        }

        private bool CheckoutExists(int id)
        {
            return _context.Checkouts.Any(e => e.CheckoutId == id);
        }
    }
}
