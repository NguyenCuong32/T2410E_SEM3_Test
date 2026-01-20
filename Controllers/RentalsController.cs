using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Models;
using System.Linq;

public class RentalsController : Controller
{
    private readonly ComicSystemContext _context;

    public RentalsController(ComicSystemContext context)
    {
        _context = context;
    }

    // GET: Rentals/Report
    public IActionResult Report(DateTime? startDate, DateTime? endDate)
    {
        var query = _context.RentalDetails
            .Include(rd => rd.Rental)
            .ThenInclude(r => r.Customer)
            .Include(rd => rd.ComicBook)
            .AsQueryable();

        if (startDate.HasValue && endDate.HasValue)
        {
            query = query.Where(rd => rd.Rental.RentalDate >= startDate 
                                   && rd.Rental.RentalDate <= endDate);
        }

        var report = query.Select(rd => new ReportViewModel
        {
            BookName = rd.ComicBook.Title,
            RentalDate = rd.Rental.RentalDate,
            ReturnDate = rd.Rental.ReturnDate,
            CustomerName = rd.Rental.Customer.FullName,
            Quantity = rd.Quantity
        }).ToList();

        return View(report);
    }
}