using ASM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASM.Controllers
{
    public class RentalDetailsMvcController : Controller
    {
        private readonly ASMContext _context;

        public RentalDetailsMvcController(ASMContext context)
        {
            _context = context;
        }

        // GET: /RentalDetailsMvc/Index
        public IActionResult Index()
        {
            var details = _context.RentalDetails
                .Include(d => d.Rental)
                .Include(d => d.ComicBook)
                .ToList();

            return View(details);
        }

        // GET: /RentalDetailsMvc/Create
        public IActionResult Create()
        {
            ViewBag.Rentals = new SelectList(
                _context.Rentals,
                "RentalID",
                "RentalID"
            );

            ViewBag.ComicBooks = new SelectList(
                _context.ComicBooks,
                "ComicBookID",
                "Title"
            );

            return View();
        }

        // POST: /RentalDetailsMvc/Create
        [HttpPost]
        public IActionResult Create(RentalDetail detail)
        {
            _context.RentalDetails.Add(detail);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
