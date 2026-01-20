using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models
{
    public class Rental
    {
        [Key]
        public int RentalID { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Rental Date")]
        public DateTime RentalDate { get; set; } = DateTime.Now;

        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Active";

        // Navigation properties
        public Customer Customer { get; set; } = null!;
        public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
