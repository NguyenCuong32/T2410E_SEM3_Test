using ComicSysApi.Data;
using ComicSysApi.Dtos;
using ComicSysApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSysApi.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ComicSystemDbContext _db;
    public CustomersController(ComicSystemDbContext db) => _db = db;

    [HttpPost("register")]
    public async Task<IActionResult> Register(CustomerRegisterDto dto)
    {
        var customer = new Customer
        {
            FullName = dto.FullName,
            PhoneNumber = dto.PhoneNumber,
            RegistrationDate = DateTime.Now
        };

        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();

        
        return Ok(new { customer.CustomerID, customer.FullName, customer.PhoneNumber, customer.RegistrationDate });
    }

    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var c = await _db.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerID == id);
        return c is null ? NotFound() : Ok(c);
    }
}
