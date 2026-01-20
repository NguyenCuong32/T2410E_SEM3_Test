namespace ComicSystem.Models
{
public class Rental
{
    public int Id { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime ReturnDate { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public ICollection<RentalDetail> RentalDetails { get; set; }
}
}
