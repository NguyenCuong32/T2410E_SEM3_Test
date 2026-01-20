using ComicSystem.Data;
using Microsoft.AspNetCore.Mvc;

public class CustomersController : Controller
{
    private readonly ComicSystemContext _context;

    public CustomersController(ComicSystemContext context)
    {
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(Customer customer)
    {
        customer.RegistrationDate = DateTime.Now;
        _context.Customers.Add(customer);
        _context.SaveChanges();
        return RedirectToAction("Index", "ComicBooks");
    }
}
