using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firebase.Auth;
using Newtonsoft.Json;
using BookShelfHaven6Ice2.Models;


namespace BookShelfHaven5.Controllers
{
    public class RegisterController : Controller
    {
        FirebaseAuthProvider auth;

        private readonly BookShelfHavenContext _context;

        public RegisterController(BookShelfHavenContext context)
        {
            auth = new FirebaseAuthProvider(new FirebaseConfig("YOUR_FIREBASE_API_KEY"));
            _context = context;
        }

        // GET: Register/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Register/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,FirstName,LastName,Email,Password")] Customer customer)
        {
            try
            {
                // Create the user in Firebase Authentication
                var fbAuthLink = await auth.CreateUserWithEmailAndPasswordAsync(customer.Email, customer.PasswordHash);

                // Get the Firebase token
                string token = fbAuthLink.FirebaseToken;

                // Save the token in a session variable
                if (token != null && ModelState.IsValid)
                {
                    HttpContext.Session.SetString("_UserToken", token);

                    // Add the new customer to the database
                    _context.Add(customer);
                    await _context.SaveChangesAsync();

                    // Redirect to the login page
                    return RedirectToAction("Create", "Login");
                }
            }
            catch (FirebaseAuthException ex)
            {
                // Assuming FirebaseError is part of FirebaseAuthException
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseAuthException>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.Message); // Use 'Message' instead of 'message'
                return View(customer);
            }



            return View();
        }

        // Other actions (Edit, Delete, etc.) can be implemented similarly as per your requirements
    }
}
