public class ComicBook
{
    public int ComicBookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal PricePerDay { get; set; }

    public ICollection<RentalDetail> RentalDetails { get; set; }
}
