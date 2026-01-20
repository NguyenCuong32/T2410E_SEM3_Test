using ASM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace ASM.Controllers
{
    public class CustomersMvcController : Controller
    {
        private readonly ASMContext _context;

        public CustomersMvcController(ASMContext context)
        {
            _context = context;
        }

        // GET: /CustomersMvc
        public IActionResult Index()
        {
            var customers = _context.Customers.ToList();
            return View("~/Views/Home/Index.cshtml", customers);
        }

        // GET: /CustomersMvc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /CustomersMvc/Create
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            customer.RegistrationDate = DateTime.Now;

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
