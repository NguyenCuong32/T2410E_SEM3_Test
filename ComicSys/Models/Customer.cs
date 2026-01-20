public class Customer
{
    public int CustomerID { get; set; }

    public required string FullName { get; set; }
    public required string PhoneNumber { get; set; }

    public DateTime RegistrationDate { get; set; }

    public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
}
