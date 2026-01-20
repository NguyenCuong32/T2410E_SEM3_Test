using System.ComponentModel.DataAnnotations;

namespace ComicSystem.DTOs
{
    public class RentalCreateDto
    {
        [Required(ErrorMessage = "Customer is required")]
        [Display(Name = "Customer")]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Comic book is required")]
        [Display(Name = "Comic Book")]
        public int ComicBookID { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price per day is required")]
        [Range(0.01, 1000000, ErrorMessage = "Price per day must be greater than 0")]
        [Display(Name = "Price Per Day")]
        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = "Rental date is required")]
        [Display(Name = "Rental Date")]
        public DateTime RentalDate { get; set; } = DateTime.Now;

        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }
    }
}
