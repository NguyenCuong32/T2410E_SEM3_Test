using ComicSystem.Models;
using ComicSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ComicSystemContext _context;

        public ReportsController(ComicSystemContext context)
        {
            _context = context;
        }

        public IActionResult Index(DateTime? start, DateTime? end)
        {
            var data =
                from rd in _context.RentalDetails
                join r in _context.Rentals on rd.RentalId equals r.RentalId
                join c in _context.Customers on r.CustomerId equals c.CustomerId
                join b in _context.ComicBooks on rd.ComicBookId equals b.ComicBookId
                where (!start.HasValue || r.RentalDate >= start.Value)
                   && (!end.HasValue || r.ReturnDate <= end.Value)
                select new ReportVM
                {
                    BookName = b.Title,          // ✅ ĐÚNG PROPERTY
                    RentalDate = r.RentalDate,
                    ReturnDate = r.ReturnDate,
                    CustomerName = c.FullName,
                    Quantity = rd.Quantity
                };

            return View(data.ToList());
        }
    }
}
