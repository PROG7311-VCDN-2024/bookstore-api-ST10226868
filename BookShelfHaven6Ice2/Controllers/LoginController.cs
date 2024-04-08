using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firebase.Auth;
using Newtonsoft.Json;
using BookShelfHaven6Ice2.Models;

namespace BookShelfHaven5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly FirebaseAuthProvider _auth;
        private readonly BookShelfHavenContext _context;

        public LoginController(BookShelfHavenContext context)
        {
            _auth = new FirebaseAuthProvider(new FirebaseConfig("Your_Firebase_Config"));
            _context = context;
        }

        // POST: api/Login/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create(Customer customer)
        {
            try
            {
                // Authenticate user using Firebase
                var fbAuthLink = await _auth.SignInWithEmailAndPasswordAsync(customer.Email, customer.PasswordHash);
                string token = fbAuthLink.FirebaseToken;

                // Save the token to a session variable

                if (customer.Email.Contains("admin", StringComparison.OrdinalIgnoreCase))
                {
                    // Redirect to the index page of the Admin controller
                    return RedirectToAction("Index", "AddAdmin");
                }

                if (token != null)
                {
                    // Redirect to the homepage or another appropriate page
                    return RedirectToAction("Index", "HomePage");
                }
            }
            catch (FirebaseAuthException ex)
            {
                string errorMessage = "An error occurred during authentication.";

                // Check if the response data is available and parse it
                if (!string.IsNullOrEmpty(ex.ResponseData))
                {
                    try
                    {
                        var errorData = JsonConvert.DeserializeObject<Dictionary<string, string>>(ex.ResponseData);
                        if (errorData != null && errorData.TryGetValue("error", out string error))
                        {
                            errorMessage = error;
                        }
                    }
                    catch (Exception)
                    {
                        // Failed to deserialize error data, use default error message
                        ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                    }
                }

                ModelState.AddModelError(string.Empty, errorMessage);
                return BadRequest(ModelState);
            }

            return BadRequest("Invalid credentials");
        }

        // Other actions (Edit, Delete, etc.) remain the same
        // Ensure proper authentication and authorization mechanisms
    }
}
