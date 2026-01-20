using ASM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ASMContext _context;

        public CustomersController(ASMContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
        }

        // POST: api/customers
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            customer.RegistrationDate = DateTime.Now;

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return Ok(customer);
        }
    }
}
