using System;
using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models.ViewModels
{
    public class RentalCreateVM
    {
        [Required]
        public int CustomerID { get; set; }

        [Required]
        public int ComicBookID { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }
    }
}
