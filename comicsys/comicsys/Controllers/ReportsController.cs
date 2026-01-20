using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;

namespace ComicSystem.Controllers
{
	public class ReportsController : Controller
	{
		private readonly ComicSystemContext _context;

		public ReportsController(ComicSystemContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View(new List<ReportViewModel>());
		}

		[HttpPost]
		public async Task<IActionResult> Index(DateTime startDate, DateTime endDate)
		{
			var query = from rd in _context.RentalDetails
						join r in _context.Rentals on rd.RentalID equals r.RentalID
						join cb in _context.ComicBooks on rd.ComicBookID equals cb.ComicBookID
						join c in _context.Customers on r.CustomerID equals c.CustomerID
						where r.RentalDate >= startDate && r.RentalDate <= endDate
						select new ReportViewModel
						{
							BookName = cb.Title,
							RentalDate = r.RentalDate,
							ReturnDate = r.ReturnDate,
							CustomerName = c.FullName,
							Quantity = rd.Quantity
						};

			var report = await query.ToListAsync();
			return View(report);
		}
	}
}