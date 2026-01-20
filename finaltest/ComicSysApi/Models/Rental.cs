using System.ComponentModel.DataAnnotations;

namespace ComicSysApi.Models;

public class Rental
{
    public int RentalID { get; set; }

   
    public int CustomerID { get; set; }
    public Customer? Customer { get; set; }

    public DateTime RentalDate { get; set; } = DateTime.Now;

    public DateTime ReturnDate { get; set; }

    [Required, StringLength(50)]
    public string Status { get; set; } = "Dang thue";

    public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
}
