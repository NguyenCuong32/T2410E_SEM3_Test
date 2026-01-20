using Microsoft.AspNetCore.Mvc;
using ComicRentalApp.Models;
using ComicRentalApp.Services;
using System.Threading.Tasks;

namespace ComicRentalApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly MongoDbService _mongoDbService;

        // Constructor
        public CustomersController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        // Register GET action - Display the registration form
        public IActionResult Register() => View();

        // Register POST action - Handle form submission and save the customer
        [HttpPost]
        public async Task<IActionResult> Register(Customer customer)
        {
            if (ModelState.IsValid) // Ensure the model is valid
            {
                customer.RegistrationDate = DateTime.Now; // Set registration date to now
                await _mongoDbService.CreateCustomer(customer); // Save customer to database
                return RedirectToAction("Index", "Home"); // Redirect to home page after successful registration
            }
            return View(customer); // Return to the form with validation errors if any
        }
    }
}
