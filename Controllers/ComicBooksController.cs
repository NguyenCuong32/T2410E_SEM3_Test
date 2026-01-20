using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Models;

namespace ComicSystem.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly ComicSystemContext _context;

        public ComicBooksController(ComicSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.ComicBooks.ToListAsync();
            return View(data);
        }
    }
}