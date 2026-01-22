using System.ComponentModel.DataAnnotations;

namespace MiniShop.Api.Models;

public class Customer
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}
