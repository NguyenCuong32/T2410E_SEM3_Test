using ASM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly ASMContext _context;

        public RentalsController(ASMContext context)
        {
            _context = context;
        }

        // GET: api/rentals
        [HttpGet]
        public IActionResult GetAll()
        {
            var rentals = _context.Rentals
                .Include(r => r.Customer)
                .ToList();

            return Ok(rentals);
        }

        // POST: api/rentals
        [HttpPost]
        public IActionResult Create(Rental rental)
        {
            rental.RentalDate = DateTime.Now;
            rental.Status = "Rented";

            _context.Rentals.Add(rental);
            _context.SaveChanges();

            return Ok(rental);
        }
    }
}
