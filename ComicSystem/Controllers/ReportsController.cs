using ComicSystem.Data;
using ComicSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers;

public class ReportsController : Controller
{
    private readonly ComicSystemContext _context;
    public ReportsController(ComicSystemContext context) => _context = context;

    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Index(DateTime startDate, DateTime endDate)
    {
        if (endDate < startDate)
        {
            ModelState.AddModelError("", "End date must be >= Start date");
            return View();
        }

        var rows = await _context.RentalDetails
            .Include(d => d.Rental)!.ThenInclude(r => r.Customer)
            .Include(d => d.ComicBook)
            .Where(d => d.Rental!.RentalDate.Date >= startDate.Date
                     && d.Rental.RentalDate.Date <= endDate.Date)
            .Select(d => new RentalReportRowVM
            {
                BookName = d.ComicBook!.Title,
                RentalDate = d.Rental!.RentalDate,
                ReturnDate = d.Rental!.ReturnDate,
                CustomerName = d.Rental!.Customer!.FullName,
                Quantity = d.Quantity
            })
            .OrderBy(x => x.RentalDate)
            .ToListAsync();

        ViewBag.StartDate = startDate;
        ViewBag.EndDate = endDate;
        return View(rows);
    }
}
