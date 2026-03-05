using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BattleGame.Functions.Models;

[Table("Asset")]
public class Asset
{
    [Key]
    public Guid AssetId { get; set; } = Guid.NewGuid();
    public string? AssetName { get; set; }
    public int? LevelRequire { get; set; }

    public ICollection<PlayerAsset> PlayerAssets { get; set; } = new List<PlayerAsset>();
}
