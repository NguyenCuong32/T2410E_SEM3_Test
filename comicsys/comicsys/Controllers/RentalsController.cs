using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;

namespace ComicSystem.Controllers
{
	public class RentalsController : Controller
	{
		private readonly ComicSystemContext _context;

		public RentalsController(ComicSystemContext context)
		{
			_context = context;
		}

		public IActionResult Create()
		{
			ViewBag.Customers = new SelectList(_context.Customers, "CustomerID", "FullName");
			ViewBag.ComicBooks = new SelectList(_context.ComicBooks, "ComicBookID", "Title");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(int CustomerID, int ComicBookID, int Quantity, DateTime RentalDate, DateTime ReturnDate)
		{
			if (Quantity <= 0 || RentalDate >= ReturnDate)
			{
				ModelState.AddModelError("", "Dữ liệu không hợp lệ.");
			}

			if (ModelState.IsValid)
			{
				var rental = new Rental
				{
					CustomerID = CustomerID,
					RentalDate = RentalDate,
					ReturnDate = ReturnDate,
					Status = "Rented"
				};
				_context.Add(rental);
				await _context.SaveChangesAsync();

				var book = await _context.ComicBooks.FindAsync(ComicBookID);
				var detail = new RentalDetail
				{
					RentalID = rental.RentalID,
					ComicBookID = ComicBookID,
					Quantity = Quantity,
					PricePerDay = book.PricePerDay
				};
				_context.Add(detail);
				await _context.SaveChangesAsync();

				return RedirectToAction("Index", "Home");
			}

			ViewBag.Customers = new SelectList(_context.Customers, "CustomerID", "FullName");
			ViewBag.ComicBooks = new SelectList(_context.ComicBooks, "ComicBookID", "Title");
			return View();
		}
	}
}