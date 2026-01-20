using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ComicBooksController : Controller
{
    private readonly ApplicationDbContext _context;

    public ComicBooksController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: ComicBooks
    public async Task<IActionResult> Index()
    {
        var books = await _context.ComicBooks.ToListAsync();
        return View(books);
    }

    // GET: ComicBooks/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ComicBooks/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ComicBook book)
    {
        if (!ModelState.IsValid)
        {
            return View(book);
        }

        _context.ComicBooks.Add(book);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: ComicBooks/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _context.ComicBooks.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    // POST: ComicBooks/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ComicBook book)
    {
        if (!ModelState.IsValid)
        {
            return View(book);
        }

        var existingBook = await _context.ComicBooks
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.ComicBookID == book.ComicBookID);

        if (existingBook == null)
        {
            return NotFound();
        }

        _context.Update(book);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: ComicBooks/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.ComicBooks.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        _context.ComicBooks.Remove(book);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
