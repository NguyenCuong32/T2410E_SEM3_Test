using System.ComponentModel.DataAnnotations;

namespace ComicSystem.Models
{
	public class ComicBook
	{
		public int ComicBookID { get; set; }

		[Required]
		public string Title { get; set; }

		public string Author { get; set; }

		[Required]
		public decimal PricePerDay { get; set; }
	}
}