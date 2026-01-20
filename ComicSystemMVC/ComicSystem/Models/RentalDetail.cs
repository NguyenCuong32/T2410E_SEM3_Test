using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models
{
    public class RentalDetail
    {
        [Key]
        public int RentalDetailID { get; set; }

        [Required]
        [ForeignKey("Rental")]
        public int RentalID { get; set; }

        [Required]
        [ForeignKey("ComicBook")]
        public int ComicBookID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price per day must be greater than 0")]
        [Display(Name = "Price Per Day")]
        public decimal PricePerDay { get; set; }

        // Navigation properties
        public Rental Rental { get; set; } = null!;
        public ComicBook ComicBook { get; set; } = null!;
    }
}
