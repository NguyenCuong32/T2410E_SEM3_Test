using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM.Models
{
    public class ComicBook
    {
        [Key]
        public int ComicBookID { get; set; }

        public string? Title { get; set; }

        public string? Author { get; set; }

        public decimal PricePerDay { get; set; }

        public ICollection<RentalDetail>? RentalDetails { get; set; }
    }
}
