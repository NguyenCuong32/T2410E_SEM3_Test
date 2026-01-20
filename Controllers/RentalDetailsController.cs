using ASM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalDetailsController : ControllerBase
    {
        private readonly ASMContext _context;

        public RentalDetailsController(ASMContext context)
        {
            _context = context;
        }

        // GET: api/rentaldetails
        [HttpGet]
        public IActionResult GetAll()
        {
            var details = _context.RentalDetails
                .Include(d => d.Rental)
                .Include(d => d.ComicBook)
                .ToList();

            return Ok(details);
        }

        // POST: api/rentaldetails
        [HttpPost]
        public IActionResult Create(RentalDetail detail)
        {
            _context.RentalDetails.Add(detail);
            _context.SaveChanges();

            return Ok(detail);
        }
    }
}
