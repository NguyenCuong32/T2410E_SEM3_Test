using ComicSystem.Data;
using ComicSystem.Models;
using ComicSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers;

public class RentalsController : Controller
{
    private readonly ComicSystemContext _context;
    public RentalsController(ComicSystemContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var rentals = await _context.Rentals
            .Include(r => r.Customer)
            .Include(r => r.Details).ThenInclude(d => d.ComicBook)
            .OrderByDescending(r => r.RentalId)
            .ToListAsync();

        return View(rentals);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Customers = new SelectList(await _context.Customers.ToListAsync(), "CustomerId", "FullName");
        ViewBag.ComicBooks = new SelectList(await _context.ComicBooks.ToListAsync(), "ComicBookId", "Title");

        var vm = new RentalCreateVM();
        vm.Items.Add(new RentalItemVM()); // 1 dòng mặc định
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RentalCreateVM vm)
    {
        if (vm.Items == null || vm.Items.Count == 0)
            ModelState.AddModelError("", "Please add at least 1 comic book.");

        if (!ModelState.IsValid)
        {
            ViewBag.Customers = new SelectList(await _context.Customers.ToListAsync(), "CustomerId", "FullName");
            ViewBag.ComicBooks = new SelectList(await _context.ComicBooks.ToListAsync(), "ComicBookId", "Title");
            return View(vm);
        }

        var rental = new Rental
        {
            CustomerId = vm.CustomerId,
            RentalDate = vm.RentalDate,
            ReturnDate = vm.ReturnDate,
            Status = "Rented"
        };

        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();

        foreach (var item in vm.Items.Where(i => i.ComicBookId > 0 && i.Quantity > 0))
        {
            _context.RentalDetails.Add(new RentalDetail
            {
                RentalId = rental.RentalId,
                ComicBookId = item.ComicBookId,
                Quantity = item.Quantity,
                PricePerDay = item.PricePerDay
            });
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
