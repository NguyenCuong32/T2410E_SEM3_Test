using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class RentalsController : Controller
{
    private readonly ApplicationDbContext _context;

    public RentalsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Rentals/Create
    public IActionResult Create()
    {
        ViewBag.Customers = _context.Customers.ToList();
        ViewBag.Books = _context.ComicBooks.ToList();
        return View();
    }

    // POST: Rentals/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RentViewModel model)
    {
        // Validate basic input
        if (!ModelState.IsValid)
        {
            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.Books = _context.ComicBooks.ToList();
            return View(model);
        }

        // Check customer exists
        var customerExists = await _context.Customers
            .AnyAsync(c => c.CustomerID == model.CustomerID);

        if (!customerExists)
        {
            return NotFound("Customer not found");
        }

        // Check book exists
        var book = await _context.ComicBooks
            .FirstOrDefaultAsync(b => b.ComicBookID == model.ComicBookID);

        if (book == null)
        {
            return NotFound("Comic book not found");
        }

        // Create Rental
        var rental = new Rental
        {
            CustomerID = model.CustomerID,
            RentalDate = model.RentalDate,
            ReturnDate = model.ReturnDate,
            Status = "Renting"
        };

        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();

        // Create Rental Detail
        var rentalDetail = new RentalDetail
        {
            RentalID = rental.RentalID,
            ComicBookID = model.ComicBookID,
            Quantity = model.Quantity,
            PricePerDay = book.PricePerDay
        };

        _context.RentalDetails.Add(rentalDetail);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "ComicBooks");
    }
}
