using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comic_Book_Rental.Models
{
    public class RentalDetail
    {
        [Key]
        public int RentalDetailID { get; set; }

        public int RentalID { get; set; }
        [ForeignKey("RentalID")]
        public virtual Rental Rental { get; set; }

        public int ComicBookID { get; set; }
        [ForeignKey("ComicBookID")]
        public virtual ComicBook ComicBook { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal PricePerDay { get; set; }
    }
}