using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models
{
    public class RentalDetail
    {
        [Key]
        public int RentalDetailId { get; set; }

        [ForeignKey("Rental")]
        public int RentalId { get; set; }

        public Rental? Rental { get; set; }

        [ForeignKey("ComicBook")]
        public int ComicBookId { get; set; }

        public ComicBook? ComicBook { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PricePerDay { get; set; }
    }
}
