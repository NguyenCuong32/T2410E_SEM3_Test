using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSysApi.Models;

public class ComicBook
{
    public int ComicBookID { get; set; }

    [Required, StringLength(255)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(255)]
    public string Author { get; set; } = string.Empty;

    [Column(TypeName = "decimal(10,2)")]
    [Range(0, 99999999)]
    public decimal PricePerDay { get; set; }

    public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
}
