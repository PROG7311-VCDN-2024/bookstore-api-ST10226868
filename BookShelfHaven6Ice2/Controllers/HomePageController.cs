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
    public class HomePageController : ControllerBase
    {
        private readonly BookShelfHavenContext _context;

        public HomePageController(BookShelfHavenContext context)
        {
            _context = context;
        }

        // GET: api/HomePage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        // GET: api/HomePage/Details/5
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

        // POST: api/HomePage/Create
        [HttpPost("Create")]
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

        // POST: api/HomePage/AddToCart
        [HttpPost("AddToCart")]
        public async Task<ActionResult> AddToCart(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            // Create a new CartItem using the Product data
            var cartItem = new Cart
            {
                // Set other properties of CartItem accordingly
                ProductId = product.ProductId,
                ProductNames = product.ProductNames,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Quantity = 1 // Assuming you're adding one item to the cart
            };

            // Add the CartItem to the database
            _context.Carts.Add(cartItem);
            await _context.SaveChangesAsync();

            return Ok("Product added to cart successfully");
        }

        // PUT: api/HomePage/Edit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest("Product ID mismatch");
            }

            if (!ProductExists(id))
            {
                return NotFound("Product not found");
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Failed to update product");
            }

            return NoContent();
        }

        // DELETE: api/HomePage/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
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
