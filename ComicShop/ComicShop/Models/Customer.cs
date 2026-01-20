namespace ComicShop.Models;

public class Customer
{
    public int customerid { get; set; }
    public string fullname { get; set; } = null!;
    public string? phonenumber { get; set; }
    public DateTime registrationdate { get; set; }
}