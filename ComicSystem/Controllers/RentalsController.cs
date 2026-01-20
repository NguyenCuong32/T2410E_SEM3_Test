using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComicSystem.Data;
using ComicSystem.Models;

public class RentalsController : Controller
{
    private readonly ApplicationDbContext _context;

    public RentalsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        ViewBag.Customers = new SelectList(_context.Customers, "Id", "FullName");
        ViewBag.Comics = new SelectList(_context.ComicBooks, "Id", "Title");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(int customerId, int comicBookId,
        DateTime rentalDate, DateTime returnDate, int quantity, decimal pricePerDay)
    {
        var rental = new Rental
        {
            CustomerId = customerId,
            RentalDate = rentalDate,
            ReturnDate = returnDate
        };

        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();

        var detail = new RentalDetail
        {
            RentalId = rental.Id,
            ComicBookId = comicBookId,
            Quantity = quantity,
            PricePerDay = pricePerDay
        };

        _context.RentalDetails.Add(detail);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }
}
