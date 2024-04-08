using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShelfHaven6Ice2.Models;

namespace BookShelfHaven5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViewOrdersController : ControllerBase
    {
        private readonly BookShelfHavenContext _context;

        public ViewOrdersController(BookShelfHavenContext context)
        {
            _context = context;
        }

        // GET: api/ViewOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts([FromQuery] string Order)
        {
            IQueryable<Cart> carts = _context.Carts.Include(c => c.Product).Include(c => c.UsernameNavigation);

            if (!string.IsNullOrEmpty(Order))
            {
                carts = carts.Where(p => p.Username.Contains(Order));
            }

            return await carts.ToListAsync();
        }

        // GET: api/ViewOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var cart = await _context.Carts
                .Include(c => c.Product)
                .Include(c => c.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.CartId == id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // POST: api/ViewOrders
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCart), new { id = cart.CartId }, cart);
        }

        // PUT: api/ViewOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, Cart cart)
        {
            if (id != cart.CartId)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ViewOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.CartId == id);
        }
    }
}
