using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models;

public class Customer
{
    public int CustomerId { get; set; }

    [Required, StringLength(255)]
    public string FullName { get; set; } = "";

    [Required, StringLength(15)]
    public string PhoneNumber { get; set; } = "";

    public DateTime RegisterDate { get; set; } = DateTime.Now;

    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
