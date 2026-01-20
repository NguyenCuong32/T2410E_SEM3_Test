namespace ComicShop.Models.DTO;

public class CreateRentalReportDTO
{
    public string bookname { get; set; } = null!;
    public DateTime rentaldate { get; set; }
    public DateTime returndate { get; set; }
    public string customername { get; set; } = null!;
    public int quantity { get; set; }
}