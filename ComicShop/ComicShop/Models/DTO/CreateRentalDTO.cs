namespace ComicShop.Models.DTO;

public class CreateRentalDTO
{
    public int customerid { get; set; }
    public DateTime rentaldate { get; set; }
    public DateTime returndate { get; set; }
    public string status { get; set; } = null!;
    public List<CreateRentalDetailDTO> rentaldetails { get; set; } = new();
}