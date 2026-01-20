using ASM.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASM.Controllers
{
    public class ComicBooksMvcController : Controller
    {
        private readonly ASMContext _context;

        public ComicBooksMvcController(ASMContext context)
        {
            _context = context;
        }

        // GET: /ComicBooksMvc/Index
        public IActionResult Index()
        {
            var comics = _context.ComicBooks.ToList();
            return View(comics);
        }

        // GET: /ComicBooksMvc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ComicBooksMvc/Create
        [HttpPost]
        public IActionResult Create(ComicBook comicBook)
        {
            _context.ComicBooks.Add(comicBook);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
