namespace ComicSystem.Models
{
public class RentalDetail
{
    public int Id { get; set; }

    public int RentalId { get; set; }
    public Rental Rental { get; set; }

    public int ComicBookId { get; set; }
    public ComicBook ComicBook { get; set; }

    public int Quantity { get; set; }
    public decimal PricePerDay { get; set; }
}
}
