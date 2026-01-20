namespace ComicSystem.Controllers;
using ComicSystem.Data;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ComicBooksController : Controller
{
    private readonly ApplicationDbContext _context;

    public ComicBooksController(ApplicationDbContext context)
    {
        _context = context;
    }


    // READ – List
    public async Task<IActionResult> Index()
    {
        var books = await _context.ComicBooks.ToListAsync();
        return View(books);
    }

    // CREATE – GET
    public IActionResult Create()
    {
        return View();
    }

    // CREATE – POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ComicBook comicBook)
    {
        if (ModelState.IsValid)
        {
            _context.ComicBooks.Add(comicBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(comicBook);
    }

    // UPDATE – GET
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _context.ComicBooks.FindAsync(id);
        if (book == null) return NotFound();
        return View(book);
    }

    // UPDATE – POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ComicBook comicBook)
    {
        if (id != comicBook.ComicBookId) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(comicBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(comicBook);
    }

    // DELETE – GET
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.ComicBooks.FirstOrDefaultAsync(x => x.ComicBookId == id);
        if (book == null) return NotFound();
        return View(book);
    }

    // DELETE – POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _context.ComicBooks.FindAsync(id);
        _context.ComicBooks.Remove(book);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
