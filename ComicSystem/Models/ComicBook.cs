using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models;

public class ComicBook
{
    public int ComicBookId { get; set; }

    [Required, StringLength(255)]
    public string Title { get; set; } = "";

    [Required, StringLength(255)]
    public string Author { get; set; } = "";

    [Column(TypeName = "decimal(10,2)")]
    public decimal PricePerDay { get; set; }

    public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
}
