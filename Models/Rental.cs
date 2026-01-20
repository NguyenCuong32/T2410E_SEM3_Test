using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM.Models
{
    public class Rental
    {
        [Key]
        public int RentalID { get; set; }

        public int CustomerID { get; set; }

        public DateTime RentalDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public string? Status { get; set; }

        [ForeignKey("CustomerID")]
        public Customer? Customer { get; set; }

        public ICollection<RentalDetail>? RentalDetails { get; set; }
    }
}
