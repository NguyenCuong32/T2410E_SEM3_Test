using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2410E_SEM3_Test.Models;

[Table("Player")]
public class Player
{
    [Key]
    public Guid PlayerId { get; set; }

    [Required]
    [MaxLength(64)]
    public string PlayerName { get; set; } = null!;

    [MaxLength(128)]
    public string? FullName { get; set; }

    [MaxLength(10)]
    public string? Age { get; set; }

    public int? Level { get; set; } = 1;

    [MaxLength(64)]
    public string? Email { get; set; }

    public ICollection<PlayerAsset> PlayerAssets { get; set; } = new List<PlayerAsset>();
}
