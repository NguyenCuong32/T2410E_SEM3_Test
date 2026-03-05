using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BattleGame.Functions.Models;

[Table("Player")]
public class Player
{
    [Key]
    public Guid PlayerId { get; set; } = Guid.NewGuid();
    public string? PlayerName { get; set; }
    public string? FullName { get; set; }
    public string? Age { get; set; }
    public int? Level { get; set; }
    public string? Email { get; set; }

    public ICollection<PlayerAsset> PlayerAssets { get; set; } = new List<PlayerAsset>();
}
