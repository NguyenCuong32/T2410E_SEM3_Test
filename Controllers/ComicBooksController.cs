using ASM.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComicBooksController : ControllerBase
    {
        private readonly ASMContext _context;

        public ComicBooksController(ASMContext context)
        {
            _context = context;
        }

        // GET: api/comicbooks
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.ComicBooks.ToList());
        }

        // POST: api/comicbooks
        [HttpPost]
        public IActionResult Create(ComicBook comicBook)
        {
            _context.ComicBooks.Add(comicBook);
            _context.SaveChanges();

            return Ok(comicBook);
        }
    }
}
