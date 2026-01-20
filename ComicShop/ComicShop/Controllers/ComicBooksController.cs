using ComicShop.Models;
using ComicShop.Models.DTO;
using ComicShop.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicShop.Controllers;

[ApiController]
[Route("api/comicbooks")]
public class ComicBooksController : ControllerBase
{
    private readonly AppDbContext _context;

    public ComicBooksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _context.comicbooks.ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Create(ComicBook book)
    {
        _context.comicbooks.Add(book);
        await _context.SaveChangesAsync();
        return Ok(book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ComicBook book)
    {
        book.comicbookid = id;
        _context.comicbooks.Update(book);
        await _context.SaveChangesAsync();
        return Ok(book);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.comicbooks.FindAsync(id);
        if (book == null) return NotFound();

        _context.comicbooks.Remove(book);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _context;

    public CustomersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(Customer customer)
    {
        customer.registrationdate = DateTime.UtcNow;
        _context.customers.Add(customer);
        await _context.SaveChangesAsync();
        return Ok(customer);
    }
}
[ApiController]
[Route("api/rentals")]
public class RentalsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IRentalRepository _repo;
    public RentalsController(AppDbContext context, IRentalRepository repo)
    {
        _context = context;
        _repo = repo;
    }

    [HttpPost]
    public async Task<IActionResult> Rent(CreateRentalDTO dto)
    {
        var rental = new Rental
        {
            customerid = dto.customerid,
            rentaldate = dto.rentaldate,
            returndate = dto.returndate,
            status = dto.status,
            rentaldetail = dto.rentaldetails.Select(d => new RentalDetail
            {
                comicbookid = d.comicbookid,
                quantity = d.quantity,
                priceperday = d.priceperday
            }).ToList()
        };

        _context.rentals.Add(rental);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            rental.rentalid,
            rental.customerid,
            rental.rentaldate,
            rental.returndate,
            rental.status
        });
    }
    [HttpGet("rentals")]
    public async Task<IActionResult> GetRentalReport(
        DateTime start,
        DateTime end)
    {
        var result = await _repo.GetRentalReport(start, end);
        return Ok(result);
    }

}
