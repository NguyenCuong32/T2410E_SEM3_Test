using System.ComponentModel.DataAnnotations;

namespace ComicSysApi.Dtos;

public class RentalCreateDto
{
    [Required] public int CustomerID { get; set; }
    [Required] public DateTime RentalDate { get; set; }
    [Required] public DateTime ReturnDate { get; set; }

    [Required, MinLength(1)]
    public List<RentalItemDto> Items { get; set; } = new();
}

public class RentalItemDto
{
    [Required] public int ComicBookID { get; set; }
    [Range(1, 999)] public int Quantity { get; set; } = 1;
}
