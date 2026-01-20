using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        // Navigation property
        public ICollection<Rental>? Rentals { get; set; }
    }
}
