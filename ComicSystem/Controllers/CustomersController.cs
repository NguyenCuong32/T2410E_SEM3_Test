using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;

public class CustomersController : Controller
{
    private readonly ComicSystemContext _context;

    public CustomersController(ComicSystemContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Customer customer)
    {
        _context.Customers.Add(customer);
        _context.SaveChanges();
        return RedirectToAction("Create");
    }
}
