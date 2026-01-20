using System.ComponentModel.DataAnnotations;

namespace ComicSystem.ViewModels;

public class RentalCreateVM
{
    [Required]
    public int CustomerId { get; set; }

    [DataType(DataType.Date)]
    public DateTime RentalDate { get; set; } = DateTime.Today;

    [DataType(DataType.Date)]
    public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(1);

    public List<RentalItemVM> Items { get; set; } = new();
}

public class RentalItemVM
{
    public int ComicBookId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal PricePerDay { get; set; }
}
