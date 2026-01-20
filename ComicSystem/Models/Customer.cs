using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(150)]
        public string FullName { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = null!;

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public ICollection<Rental>? Rentals { get; set; }
    }
}
