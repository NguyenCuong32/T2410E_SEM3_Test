using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BattleGame.Functions.Models;

[Table("PlayerAsset")]
[PrimaryKey(nameof(PlayerId), nameof(AssetId))]
public class PlayerAsset
{
    public Guid PlayerId { get; set; }
    public Guid AssetId { get; set; }

    [ForeignKey(nameof(PlayerId))]
    public Player? Player { get; set; }

    [ForeignKey(nameof(AssetId))]
    public Asset? Asset { get; set; }
}
