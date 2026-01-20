using Microsoft.AspNetCore.Mvc;
using ComicRentalApp.Models;
using ComicRentalApp.Services;
using MongoDB.Driver;  // Needed for ObjectId and MongoDB operations
using System.Threading.Tasks;

namespace ComicRentalApp.Controllers
{
    public class ComicBooksController : Controller
    {
        private readonly MongoDbService _mongoDbService;

        // Constructor
        public ComicBooksController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        // Index (GET) - List all comic books
        public async Task<IActionResult> Index()
        {
            var comicBooks = await _mongoDbService.GetComicBooks(); // Fetch all comic books
            return View(comicBooks); // Pass comic books to the Index view
        }

        // Create (GET) - Display the Create form
        public IActionResult Create() => View(); // Returns the Create view for comic book

        // Create (POST) - Process the Create form and save a new comic book
        [HttpPost]
        public async Task<IActionResult> Create(ComicBook comicBook)
        {
            if (ModelState.IsValid)  // Validate the data
            {
                await _mongoDbService.CreateComicBook(comicBook); // Save new comic book
                return RedirectToAction(nameof(Index)); // Redirect to the Index page after creating
            }
            return View(comicBook); // Return to Create form with the comic book data if validation fails
        }

        // Edit (GET) - Display the Edit form with the current comic book data
        public async Task<IActionResult> Edit(string id)
        {
            var comicBook = await _mongoDbService.GetComicBookById(id); // Fetch comic book by ID
            if (comicBook == null)
            {
                return NotFound(); // Return 404 if comic book not found
            }
            return View(comicBook); // Return comic book data to the Edit view
        }

        // Edit (POST) - Process the Edit form and update the comic book
        [HttpPost]
        public async Task<IActionResult> Edit(string id, ComicBook updatedComicBook)
        {
            if (id != updatedComicBook.Id.ToString())  // Ensure ID matches
            {
                return BadRequest(); // Return bad request if IDs do not match
            }

            if (ModelState.IsValid)
            {
                var comicBook = await _mongoDbService.GetComicBookById(id); // Get the existing comic book
                if (comicBook == null)
                {
                    return NotFound(); // Return 404 if comic book not found
                }

                // Update comic book details
                comicBook.Title = updatedComicBook.Title;
                comicBook.Author = updatedComicBook.Author;
                comicBook.PricePerDay = updatedComicBook.PricePerDay;

                // Save the updated comic book
                await _mongoDbService.UpdateComicBook(comicBook);

                return RedirectToAction(nameof(Index)); // Redirect to Index after saving changes
            }
            return View(updatedComicBook); // Return to Edit form if validation fails
        }

        // Delete (GET) - Display the Delete confirmation page for a comic book
        public async Task<IActionResult> Delete(string id)
        {
            var comicBook = await _mongoDbService.GetComicBookById(id); // Fetch comic book by ID
            if (comicBook == null)
            {
                return NotFound(); // Return 404 if comic book not found
            }
            return View(comicBook); // Return comic book data to the Delete view
        }

        // Delete (POST) - Process the deletion of a comic book
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var comicBook = await _mongoDbService.GetComicBookById(id); // Fetch comic book by ID
            if (comicBook == null)
            {
                return NotFound(); // Return 404 if comic book not found
            }

            await _mongoDbService.DeleteComicBook(id); // Delete the comic book from the database

            return RedirectToAction(nameof(Index)); // Redirect to Index page after deletion
        }
    }
}
