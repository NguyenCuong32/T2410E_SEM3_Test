namespace ComicSystem.Models;
using System.ComponentModel.DataAnnotations;

public class ComicBook
{
    public int ComicBookId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Author { get; set; }

    [Required]
    [Range(0, 1000000)]
    public decimal PricePerDay { get; set; }
}

