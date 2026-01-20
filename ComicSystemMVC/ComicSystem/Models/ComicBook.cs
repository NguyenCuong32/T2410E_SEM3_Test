using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
    public class ComicBook
    {
        [Key]
        public int ComicBookID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Author { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price per day must be greater than 0")]
        public decimal PricePerDay { get; set; }

        // Navigation property
        public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
