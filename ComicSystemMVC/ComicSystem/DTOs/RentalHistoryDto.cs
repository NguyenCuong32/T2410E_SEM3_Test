namespace ComicSystem.DTOs
{
    public class RentalHistoryDto
    {
        public int RentalID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string ComicBookTitle { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PricePerDay { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalCost { get; set; }
    }
}
