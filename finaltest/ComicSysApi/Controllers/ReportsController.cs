using ComicSysApi.Data;
using ComicSysApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSysApi.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    private readonly ComicSystemDbContext _db;
    public ReportsController(ComicSystemDbContext db) => _db = db;

    
    [HttpGet("rentals")]
    public async Task<IActionResult> RentalsReport([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        if (end < start) return BadRequest("end must be >= start");

        var rows = await _db.RentalDetails
            .AsNoTracking()
            .Where(d => d.Rental!.RentalDate >= start && d.Rental.RentalDate <= end)
            .Select(d => new RentalReportRowDto(
                d.ComicBook!.Title,
                d.Rental!.RentalDate,
                d.Rental!.ReturnDate,
                d.Rental.Customer!.FullName,
                d.Quantity
            ))
            .OrderBy(x => x.RentalDate)
            .ToListAsync();

       
        var result = rows.Select((r, idx) => new
        {
            No = idx + 1,
            BookName = r.BookName,
            RentalDate = r.RentalDate,
            ReturnDate = r.ReturnDate,
            CustomerName = r.CustomerName,
            Quantity = r.Quantity
        });

        return Ok(result);
    }
}
