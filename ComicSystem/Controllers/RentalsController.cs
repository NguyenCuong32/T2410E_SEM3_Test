namespace ComicSystem.Controllers;
using ComicSystem.Data;

using ComicSystem.Models;
using ComicSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Models.ViewModels;


public class RentalsController : Controller
{
    private readonly ApplicationDbContext _context;

    public RentalsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // RENT – GET
    public IActionResult Create()
    {
        ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "FullName");
        ViewBag.Books = new SelectList(_context.ComicBooks, "ComicBookId", "Title");
        return View();
    }

    // RENT – POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        int customerId,
        int comicBookId,
        DateTime rentalDate,
        DateTime returnDate,
        int quantity)
    {
        // 1. Insert into Rentals
        var rental = new Rental
        {
            CustomerId = customerId,
            RentalDate = rentalDate,
            ReturnDate = returnDate,
            Status = "Rented"
        };

        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();

        // 2. Insert into RentalDetails
        var book = await _context.ComicBooks.FindAsync(comicBookId);

        var detail = new RentalDetail
        {
            RentalId = rental.RentalId,
            ComicBookId = comicBookId,
            Quantity = quantity,
            PricePerDay = book.PricePerDay
        };

        _context.RentalDetails.Add(detail);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "ComicBooks");
    }
    public IActionResult Report()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Report(DateTime startDate, DateTime endDate)
    {
        var data = from r in _context.Rentals
                   join rd in _context.RentalDetails
                        on r.RentalId equals rd.RentalId
                   join b in _context.ComicBooks
                        on rd.ComicBookId equals b.ComicBookId
                   join c in _context.Customers
                        on r.CustomerId equals c.CustomerId
                   where r.RentalDate >= startDate
                      && r.ReturnDate <= endDate
                   select new RentalReportVM
                   {
                       BookName = b.Title,
                       RentalDate = r.RentalDate,
                       ReturnDate = r.ReturnDate,
                       CustomerName = c.FullName,
                       Quantity = rd.Quantity
                   };

        return View(data.ToList());
    }

}

