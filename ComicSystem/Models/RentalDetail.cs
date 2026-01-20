using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models;

public class RentalDetail
{
    public int RentalDetailId { get; set; }

    public int RentalId { get; set; }
    public Rental? Rental { get; set; }

    public int ComicBookId { get; set; }
    public ComicBook? ComicBook { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal PricePerDay { get; set; }
}
