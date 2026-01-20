using ComicSysApi.Data;
using ComicSysApi.Dtos;
using ComicSysApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSysApi.Controllers;

[ApiController]
[Route("api/comicbooks")]
public class ComicBooksController : ControllerBase
{
    private readonly ComicSystemDbContext _db;
    public ComicBooksController(ComicSystemDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _db.ComicBooks.AsNoTracking().ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _db.ComicBooks.AsNoTracking().FirstOrDefaultAsync(x => x.ComicBookID == id);
        return book is null ? NotFound() : Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ComicBookCreateDto dto)
    {
        var book = new ComicBook { Title = dto.Title, Author = dto.Author, PricePerDay = dto.PricePerDay };
        _db.ComicBooks.Add(book);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = book.ComicBookID }, book);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ComicBookUpdateDto dto)
    {
        var book = await _db.ComicBooks.FindAsync(id);
        if (book is null) return NotFound();

        book.Title = dto.Title;
        book.Author = dto.Author;
        book.PricePerDay = dto.PricePerDay;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _db.ComicBooks.FindAsync(id);
        if (book is null) return NotFound();

        _db.ComicBooks.Remove(book);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
