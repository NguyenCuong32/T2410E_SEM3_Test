using ComicSystem.Data;
using Microsoft.AspNetCore.Mvc;

public class ReportsController : Controller
{
    private readonly ComicSystemContext _context;

    public ReportsController(ComicSystemContext context)
    {
        _context = context;
    }

    public IActionResult Index(DateTime? startDate, DateTime? endDate)
    {
        var data = from r in _context.Rentals
                   join rd in _context.RentalDetails on r.RentalID equals rd.RentalID
                   join c in _context.Customers on r.CustomerID equals c.CustomerID
                   join b in _context.ComicBooks on rd.ComicBookID equals b.ComicBookID
                   select new ReportVM
                   {
                       BookName = b.Title,
                       RentalDate = r.RentalDate,
                       ReturnDate = r.ReturnDate,
                       CustomerName = c.FullName,
                       Quantity = rd.Quantity
                   };

        if (startDate != null && endDate != null)
        {
            data = data.Where(x => x.RentalDate >= startDate && x.RentalDate <= endDate);
        }

        return View(data.ToList());
    }
}
