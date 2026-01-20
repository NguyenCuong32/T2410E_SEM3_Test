using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;

public class RentalsController : Controller
{
    private readonly ComicSystemContext _context;

    public RentalsController(ComicSystemContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        ViewBag.Customers = _context.Customers.ToList();
        ViewBag.ComicBooks = _context.ComicBooks.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Create(int customerId, DateTime rentalDate, DateTime returnDate, int comicBookId, int quantity)
    {
        var rental = new Rental
        {
            CustomerId = customerId,
            RentalDate = rentalDate,
            ReturnDate = returnDate
        };
        _context.Rentals.Add(rental);
        _context.SaveChanges();

        var book = _context.ComicBooks.Find(comicBookId);

        var detail = new RentalDetail
        {
            RentalId = rental.RentalId,
            ComicBookId = comicBookId,
            Quantity = quantity,
            PricePerDay = book.PricePerDay
        };
        _context.RentalDetails.Add(detail);
        _context.SaveChanges();

        return RedirectToAction("Create");
    }
}
