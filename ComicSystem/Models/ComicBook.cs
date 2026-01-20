using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models
{
    public class ComicBook
    {
        [Key]
        public int ComicBookId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(200)]
        public string? Author { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PricePerDay { get; set; }

        public int Stock { get; set; }
    }
}
