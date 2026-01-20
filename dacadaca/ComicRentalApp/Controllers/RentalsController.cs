    using Microsoft.AspNetCore.Mvc;
    using ComicRentalApp.Models;
    using ComicRentalApp.Services;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    namespace ComicRentalApp.Controllers
    {
        public class RentalsController : Controller
        {
            private readonly MongoDbService _mongoDbService;

            // Constructor
            public RentalsController(MongoDbService mongoDbService)
            {
                _mongoDbService = mongoDbService;
            }

            // RentBook GET action - Display the rental form
            public IActionResult RentBook()
            {
                return View(); // Display the rent book form
            }

            // RentBook POST action - Handle the rental form submission
            [HttpPost]
            public async Task<IActionResult> RentBook(Rental rental, List<RentalDetail> rentalDetails)
            {
                if (ModelState.IsValid) // Validate the form
                {
                    await _mongoDbService.CreateRental(rental); // Save rental info to database

                    // Save rental details (e.g., quantity, price per day for each comic)
                    foreach (var detail in rentalDetails)
                    {
                        detail.RentalId = rental.Id; // Set the rental ID for each rental detail
                        await _mongoDbService.CreateRentalDetail(detail); // Save rental detail
                    }

                    return RedirectToAction("Index", "Home"); // Redirect to home after success
                }
                return View(rental); // Return to the rental form with errors if validation fails
            }
        }
    }
