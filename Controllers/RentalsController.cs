using ComicSystem.Data;
using ComicSystem.Models;
using ComicSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComicSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ComicSystemContext _context;

        public RentalsController(ComicSystemContext context)
        {
            _context = context;
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerID", "FullName");
            ViewBag.ComicBooks = new SelectList(_context.ComicBooks, "ComicBookID", "Title");

            return View(new RentalCreateVM());
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RentalCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Customers = new SelectList(_context.Customers, "CustomerID", "FullName");
                ViewBag.ComicBooks = new SelectList(_context.ComicBooks, "ComicBookID", "Title");
                return View(model);
            }

            // 1. Tạo Rental
            var rental = new Rental
            {
                CustomerID = model.CustomerID,
                RentalDate = model.RentalDate,
                ReturnDate = model.ReturnDate,
                Status = "Renting"
            };

            _context.Rentals.Add(rental);
            _context.SaveChanges();

            // 2. Lấy ComicBook
            var comic = _context.ComicBooks.Find(model.ComicBookID);
            if (comic == null)
            {
                ModelState.AddModelError("", "Comic book not found");
                return View(model);
            }

            // 3. Tạo RentalDetail
            var detail = new RentalDetail
            {
                RentalID = rental.RentalID,
                ComicBookID = model.ComicBookID,
                Quantity = model.Quantity,
                PricePerDay = comic.PricePerDay
            };

            _context.RentalDetails.Add(detail);
            _context.SaveChanges();

            return RedirectToAction("Create");
        }
    }
}
