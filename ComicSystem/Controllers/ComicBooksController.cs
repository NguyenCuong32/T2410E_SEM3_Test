using Microsoft.AspNetCore.Mvc;
using ComicSystem.Models;

namespace ComicSystem.Controllers;

public class ComicBooksController : Controller
{
    private readonly ComicSystemContext _context;

    public ComicBooksController(ComicSystemContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.ComicBooks.ToList());
    }
}
