using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;

public class ReportController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReportController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(DateTime? start, DateTime? end)
    {
        var query = _context.RentalDetails
            .Include(d => d.Rental)
                .ThenInclude(r => r.Customer)
            .Include(d => d.ComicBook)
            .AsQueryable();

        if (start.HasValue && end.HasValue)
        {
            query = query.Where(d =>
                d.Rental.RentalDate >= start.Value &&
                d.Rental.RentalDate <= end.Value);
        }

        return View(query.ToList());
    }
}
