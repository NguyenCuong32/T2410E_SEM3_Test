using ComicSysApi.Data;
using ComicSysApi.Dtos;
using ComicSysApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSysApi.Controllers;

[ApiController]
[Route("api/rentals")]
public class RentalsController : ControllerBase
{
    private readonly ComicSystemDbContext _db;
    public RentalsController(ComicSystemDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> CreateRental(RentalCreateDto dto)
    {
        
        var customerExists = await _db.Customers.AnyAsync(c => c.CustomerID == dto.CustomerID);
        if (!customerExists) return BadRequest("CustomerID not found.");

       
        var bookIds = dto.Items.Select(i => i.ComicBookID).Distinct().ToList();
        var books = await _db.ComicBooks
            .Where(b => bookIds.Contains(b.ComicBookID))
            .ToDictionaryAsync(b => b.ComicBookID);

        if (books.Count != bookIds.Count)
            return BadRequest("One or more ComicBookID not found.");

        using var tx = await _db.Database.BeginTransactionAsync();

        var rental = new Rental
        {
            CustomerID = dto.CustomerID,
            RentalDate = dto.RentalDate,
            ReturnDate = dto.ReturnDate,
            Status = "Dang thue"
        };

        _db.Rentals.Add(rental);
        await _db.SaveChangesAsync(); 

        var details = dto.Items.Select(i => new RentalDetail
        {
            RentalID = rental.RentalID,
            ComicBookID = i.ComicBookID,
            Quantity = i.Quantity,
            PricePerDay = books[i.ComicBookID].PricePerDay
        }).ToList();

        _db.RentalDetails.AddRange(details);
        await _db.SaveChangesAsync();

        await tx.CommitAsync();

        return Ok(new
        {
            rental.RentalID,
            rental.CustomerID,
            rental.RentalDate,
            rental.ReturnDate,
            rental.Status,
            Items = details.Select(d => new { d.ComicBookID, d.Quantity, d.PricePerDay })
        });
    }
}
