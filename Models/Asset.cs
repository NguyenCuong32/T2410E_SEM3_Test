using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2410E_SEM3_Test.Models;

[Table("Asset")]
public class Asset
{
    [Key]
    public Guid AssetId { get; set; }

    [Required]
    [MaxLength(64)]
    public string AssetName { get; set; } = null!;

    public int? LevelRequire { get; set; } = 0;

    public ICollection<PlayerAsset> PlayerAssets { get; set; } = new List<PlayerAsset>();
}
