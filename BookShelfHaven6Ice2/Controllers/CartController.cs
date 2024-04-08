using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShelfHaven6Ice2.Models;

namespace BookShelfHaven6Ice2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly BookShelfHavenContext _context;

        public CartsController(BookShelfHavenContext context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public IActionResult GetCarts()
        {
            return Ok(_context.Carts);
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public IActionResult GetCart(int id)
        {
            var cart = _context.Carts.Find(id);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // POST: api/Carts
        [HttpPost]
        public IActionResult PostCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCart), new { id = cart.CartId }, cart);
        }

        // PUT: api/Carts/5
        [HttpPut("{id}")]
        public IActionResult PutCart(int id, Cart cart)
        {
            if (id != cart.CartId)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCart(int id)
        {
            var cart = _context.Carts.Find(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            _context.SaveChanges();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }
    }
}
