using Microsoft.AspNetCore.Mvc;

public class CustomersController : Controller
{
    private readonly ApplicationDbContext _context;
    public CustomersController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Customer customer)
    {
        customer.RegistrationDate = DateTime.Now;
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "ComicBooks");
    }
}
