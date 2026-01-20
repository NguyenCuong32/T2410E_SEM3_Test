using ASM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASM.Controllers
{
    public class RentalsMvcController : Controller
    {
        private readonly ASMContext _context;

        public RentalsMvcController(ASMContext context)
        {
            _context = context;
        }

        // GET: /RentalsMvc/Index
        public IActionResult Index()
        {
            var rentals = _context.Rentals
                .Include(r => r.Customer)
                .ToList();

            return View(rentals);
        }

        // GET: /RentalsMvc/Create
        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(
                _context.Customers,
                "CustomerID",
                "FullName"
            );
            return View();
        }

        // POST: /RentalsMvc/Create
        [HttpPost]
        public IActionResult Create(Rental rental)
        {
            rental.RentalDate = DateTime.Now;
            rental.Status = "Rented";

            _context.Rentals.Add(rental);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
