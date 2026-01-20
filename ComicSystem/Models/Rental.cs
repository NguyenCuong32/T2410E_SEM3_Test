using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models;

public class Rental
{
    public int RentalId { get; set; }

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public DateTime RentalDate { get; set; } = DateTime.Now;

    public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(1);

    [StringLength(50)]
    public string Status { get; set; } = "Rented";

    public ICollection<RentalDetail> Details { get; set; } = new List<RentalDetail>();
}
