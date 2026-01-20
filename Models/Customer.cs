public class Customer
{
    public int CustomerId { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime RegistrationDate { get; set; }

    public ICollection<Rental> Rentals { get; set; }
}
