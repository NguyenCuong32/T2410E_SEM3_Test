using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;
using ComicSystem.DTOs;

namespace ComicSystem.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ComicSystemDbContext _context;

        public CustomersController(ComicSystemDbContext context)
        {
            _context = context;
        }

        // GET: Customers/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Customers/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CustomerRegisterDto dto)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    FullName = dto.FullName,
                    PhoneNumber = dto.PhoneNumber,
                    RegistrationDate = DateTime.Now
                };

                _context.Add(customer);
                await _context.SaveChangesAsync();
                
                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("Index", "Home");
            }
            return View(dto);
        }

        // GET: Customers (Optional - to view all customers)
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers);
        }
    }
}
