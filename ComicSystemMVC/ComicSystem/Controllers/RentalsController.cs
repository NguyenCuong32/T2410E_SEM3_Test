using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComicSystem.Data;
using ComicSystem.Models;
using ComicSystem.DTOs;

namespace ComicSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ComicSystemDbContext _context;

        public RentalsController(ComicSystemDbContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var rentalHistory = await _context.RentalDetails
                .Include(rd => rd.Rental)
                .ThenInclude(r => r.Customer)
                .Include(rd => rd.ComicBook)
                .Select(rd => new RentalHistoryDto
                {
                    RentalID = rd.RentalID,
                    CustomerName = rd.Rental.Customer.FullName,
                    ComicBookTitle = rd.ComicBook.Title,
                    Quantity = rd.Quantity,
                    PricePerDay = rd.PricePerDay,
                    RentalDate = rd.Rental.RentalDate,
                    ReturnDate = rd.Rental.ReturnDate,
                    Status = rd.Rental.Status,
                    TotalCost = rd.Quantity * rd.PricePerDay * 
                                (rd.Rental.ReturnDate.HasValue ? 
                                (decimal)(rd.Rental.ReturnDate.Value - rd.Rental.RentalDate).TotalDays : 
                                (decimal)(DateTime.Now - rd.Rental.RentalDate).TotalDays)
                })
                .ToListAsync();

            return View(rentalHistory);
        }

        // GET: Rentals/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Customers = new SelectList(await _context.Customers.ToListAsync(), "CustomerID", "FullName");
            ViewBag.ComicBooks = new SelectList(await _context.ComicBooks.ToListAsync(), "ComicBookID", "Title");
            return View();
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                // Get the comic book to retrieve its price
                var comicBook = await _context.ComicBooks.FindAsync(dto.ComicBookID);
                if (comicBook == null)
                {
                    ModelState.AddModelError("", "Comic book not found");
                    ViewBag.Customers = new SelectList(await _context.Customers.ToListAsync(), "CustomerID", "FullName", dto.CustomerID);
                    ViewBag.ComicBooks = new SelectList(await _context.ComicBooks.ToListAsync(), "ComicBookID", "Title", dto.ComicBookID);
                    return View(dto);
                }

                // Create the rental
                var rental = new Rental
                {
                    CustomerID = dto.CustomerID,
                    RentalDate = dto.RentalDate,
                    ReturnDate = dto.ReturnDate,
                    Status = "Active"
                };

                _context.Rentals.Add(rental);
                await _context.SaveChangesAsync();

                // Create the rental detail
                var rentalDetail = new RentalDetail
                {
                    RentalID = rental.RentalID,
                    ComicBookID = dto.ComicBookID,
                    Quantity = dto.Quantity,
                    PricePerDay = comicBook.PricePerDay
                };

                _context.RentalDetails.Add(rentalDetail);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Rental created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Customers = new SelectList(await _context.Customers.ToListAsync(), "CustomerID", "FullName", dto.CustomerID);
            ViewBag.ComicBooks = new SelectList(await _context.ComicBooks.ToListAsync(), "ComicBookID", "Title", dto.ComicBookID);
            return View(dto);
        }

        // GET: Rentals/Report
        public IActionResult Report()
        {
            return View(new RentalReportFilterDto());
        }

        // POST: Rentals/Report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Report(RentalReportFilterDto filter)
        {
            var query = _context.RentalDetails
                .Include(rd => rd.Rental)
                .ThenInclude(r => r.Customer)
                .Include(rd => rd.ComicBook)
                .AsQueryable();

            // Apply date filters if provided
            if (filter.StartDate.HasValue)
            {
                query = query.Where(rd => rd.Rental.RentalDate >= filter.StartDate.Value);
            }

            if (filter.EndDate.HasValue)
            {
                query = query.Where(rd => rd.Rental.RentalDate <= filter.EndDate.Value);
            }

            var reportData = await query
                .OrderBy(rd => rd.Rental.RentalDate)
                .ToListAsync();

            // Map to DTO with sequential numbering
            var result = reportData.Select((rd, index) => new RentalReportDto
            {
                No = index + 1,
                BookName = rd.ComicBook.Title,
                RentalDate = rd.Rental.RentalDate,
                ReturnDate = rd.Rental.ReturnDate,
                CustomerName = rd.Rental.Customer.FullName,
                Quantity = rd.Quantity
            }).ToList();

            ViewBag.Filter = filter;
            return View("ReportResult", result);
        }
    }
}
