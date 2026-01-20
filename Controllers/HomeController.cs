using ASM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ASM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ASMContext _context;

        public HomeController(ASMContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            model.Customers = _context.Customers.ToList();
            model.ComicBooks = _context.ComicBooks.ToList();
            model.Rentals = _context.Rentals.ToList();
            model.RentalDetails = _context.RentalDetails.ToList();

            return View(model);
        }
    }
}
