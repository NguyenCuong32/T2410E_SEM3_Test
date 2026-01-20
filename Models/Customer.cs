using System.ComponentModel.DataAnnotations;

namespace Comic_Book_Rental.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}