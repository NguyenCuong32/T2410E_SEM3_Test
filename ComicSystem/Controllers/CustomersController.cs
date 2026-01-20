namespace ComicSystem.Controllers;
using ComicSystem.Data;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CustomersController : Controller
{
    private readonly ApplicationDbContext _context;

    public CustomersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // REGISTER – GET
    public IActionResult Register()
    {
        return View();
    }

    // REGISTER – POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(Customer customer)
    {
        if (ModelState.IsValid)
        {
            customer.RegistrationDate = DateTime.Now;

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Sau khi đăng ký xong → về trang thuê sách hoặc danh sách truyện
            return RedirectToAction("Index", "ComicBooks");
        }

        return View(customer);
    }
}
