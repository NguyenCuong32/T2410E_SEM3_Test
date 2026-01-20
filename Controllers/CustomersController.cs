using Microsoft.AspNetCore.Mvc;

public class CustomersController : Controller
{
    private readonly ComicSystemContext _context;

    public CustomersController(ComicSystemContext context)
    {
        _context = context;
    }

    // GET: Customers/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Customers/Create
    [HttpPost]
    public IActionResult Create(Customer customer)
    {
        customer.RegistrationDate = DateTime.Now;
        _context.Customers.Add(customer);
        _context.SaveChanges();

        return RedirectToAction("Index", "ComicBooks");
    }
}
