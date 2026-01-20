namespace ComicShop.Models;

public class Rental
{
    public int rentalid { get; set; }
    public int customerid { get; set; }
    public DateTime rentaldate { get; set; }
    public DateTime returndate { get; set; }
    public string status { get; set; } = null!;

    public Customer customer { get; set; } = null!;
    public ICollection<RentalDetail> rentaldetail { get; set; } = new List<RentalDetail>();
}