using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShelfHaven6Ice2.Models;

namespace BookShelfHaven6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddInventoryController : ControllerBase
    {
        private readonly BookShelfHavenContext _context;

        public AddInventoryController(BookShelfHavenContext context)
        {
            _context = context;
        }

        // GET: api/AddInventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string inventoryCheck)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(inventoryCheck))
            {
                products = products.Where(p => p.ProductNames.Contains(inventoryCheck));
            }

            return await products.ToListAsync();
        }

        // GET: api/AddInventory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/AddInventory
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/AddInventory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // DELETE: api/AddInventory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
