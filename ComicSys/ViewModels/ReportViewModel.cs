public class ReportViewModel
{
    public required string BookName { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public required string CustomerName { get; set; }
    public int Quantity { get; set; }
}
