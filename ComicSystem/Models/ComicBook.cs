using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models
{
    public class ComicBook
    {
        [Key]
        public int ComicBookID { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Author { get; set; } = string.Empty;
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PricePerDay { get; set; }
    }
}