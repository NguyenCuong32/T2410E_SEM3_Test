namespace ComicShop.Models;

public class RentalDetail
{
    public int rentaldetailid { get; set; }
    public int rentalid { get; set; }
    public int comicbookid { get; set; }
    public int quantity { get; set; }
    public decimal priceperday { get; set; }

    public Rental rental { get; set; } = null!;
    public ComicBook comicbook { get; set; } = null!;
}