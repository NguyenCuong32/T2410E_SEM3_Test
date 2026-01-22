using System.ComponentModel.DataAnnotations;

namespace MiniShop.Api.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}

public class ProductCreateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}

public class ProductUpdateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
