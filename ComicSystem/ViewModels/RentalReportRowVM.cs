namespace ComicSystem.ViewModels;

public class RentalReportRowVM
{
    public string BookName { get; set; } = "";
    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string CustomerName { get; set; } = "";
    public int Quantity { get; set; }
}
