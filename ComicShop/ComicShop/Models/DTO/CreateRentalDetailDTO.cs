namespace ComicShop.Models.DTO;

public class CreateRentalDetailDTO
{
    public int comicbookid { get; set; }
    public int quantity { get; set; }
    public decimal priceperday { get; set; }
}
