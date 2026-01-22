using System.ComponentModel.DataAnnotations;

namespace MiniShop.Api.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
