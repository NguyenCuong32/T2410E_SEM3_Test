namespace ComicShop.Models;

public class ComicBook
{
    public int comicbookid { get; set; }  
    public string title { get; set; } = null!;
    public string author { get; set; } = null!;
    public decimal priceperday { get; set; }
}