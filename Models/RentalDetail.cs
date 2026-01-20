using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM.Models
{
    public class RentalDetail
    {
        [Key]
        public int RentalDetailID { get; set; }

        public int RentalID { get; set; }

        public int ComicBookID { get; set; }

        public int Quantity { get; set; }

        public decimal PricePerDay { get; set; }

        [ForeignKey(nameof(RentalID))]
        public Rental Rental { get; set; }

        [ForeignKey(nameof(ComicBookID))]
        public ComicBook ComicBook { get; set; }
    }
}
