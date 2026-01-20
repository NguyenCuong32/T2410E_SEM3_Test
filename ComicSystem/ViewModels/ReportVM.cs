namespace ComicSystem.ViewModels
{
    public class ReportVM
    {
        public string BookName { get; set; } = null!;
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string CustomerName { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
