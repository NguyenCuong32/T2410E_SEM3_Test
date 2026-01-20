public class ComicBook
{
    public int ComicBookID { get; set; }

    public required string Title { get; set; }
    public required string Author { get; set; }

    public decimal PricePerDay { get; set; }

    public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
}
