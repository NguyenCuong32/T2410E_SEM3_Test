using Microsoft.AspNetCore.Mvc;

public class ReportsController : Controller
{
    private readonly ApplicationDbContext _context;
    public ReportsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(DateTime start, DateTime end)
    {
        var data =
            from r in _context.Rentals
            join rd in _context.RentalDetails on r.RentalID equals rd.RentalID
            join b in _context.ComicBooks on rd.ComicBookID equals b.ComicBookID
            join c in _context.Customers on r.CustomerID equals c.CustomerID
            where r.RentalDate >= start && r.RentalDate <= end
            select new ReportViewModel
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
