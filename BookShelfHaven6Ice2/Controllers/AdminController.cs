using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShelfHaven6Ice2.Models;

namespace BookShelfHaven6Ice2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly BookShelfHavenContext _context;

        public AdminsController(BookShelfHavenContext context)
        {
            _context = context;
        }

        // GET: api/Admins
        [HttpGet]
        public IActionResult GetAdmins()
        {
            return Ok(_context.Admins);
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public IActionResult GetAdmin(string id)
        {
            var admin = _context.Admins.Find(id);

            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }

        // POST: api/Admins
        [HttpPost]
        public IActionResult PostAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAdmin), new { id = admin.Username }, admin);
        }

        // PUT: api/Admins/5
        [HttpPut("{id}")]
        public IActionResult PutAdmin(string id, Admin admin)
        {
            if (id != admin.Username)
            {
                return BadRequest();
            }

            _context.Entry(admin).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(string id)
        {
            var admin = _context.Admins.Find(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            _context.SaveChanges();

            return NoContent();
        }

        private bool AdminExists(string id)
        {
            return _context.Admins.Any(e => e.Username == id);
        }
    }
}
