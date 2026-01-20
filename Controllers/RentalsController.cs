using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ComicSystem.Models;   // QUAN TRỌNG
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ComicSystemContext _context;

        public RentalsController(ComicSystemContext context)
        {
            _context = context;
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(
                _context.Customers.ToList(),
                "CustomerId",
                "FullName"
            );

            ViewBag.ComicBooks = new SelectList(
                _context.ComicBooks.ToList(),
                "ComicBookId",
                "Title"
            );

            return View();
        }

        // POST: Rentals/Create
        [HttpPost]
        public IActionResult Create(
            int customerId,
            int comicBookId,
            int quantity,
            DateTime rentalDate,
            DateTime returnDate)
        {
            // 1. Ins
