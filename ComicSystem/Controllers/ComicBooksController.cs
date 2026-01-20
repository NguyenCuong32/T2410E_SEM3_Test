using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;

public class ComicBooksController : Controller
{
    private readonly ApplicationDbContext _context;

    public ComicBooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.ComicBooks.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ComicBook comic)
    {
        if (ModelState.IsValid)
        {
            _context.Add(comic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(comic);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var comic = await _context.ComicBooks.FindAsync(id);
        return View(comic);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ComicBook comic)
    {
        _context.Update(comic);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var comic = await _context.ComicBooks.FindAsync(id);
        return View(comic);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var comic = await _context.ComicBooks.FindAsync(id);
        _context.ComicBooks.Remove(comic);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
