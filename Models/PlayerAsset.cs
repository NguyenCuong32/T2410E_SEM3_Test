using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2410E_SEM3_Test.Models;

[Table("PlayerAsset")]
public class PlayerAsset
{
    public Guid PlayerId { get; set; }
    public Guid AssetId { get; set; }

    [ForeignKey("PlayerId")]
    public Player Player { get; set; } = null!;

    [ForeignKey("AssetId")]
    public Asset Asset { get; set; } = null!;
}
