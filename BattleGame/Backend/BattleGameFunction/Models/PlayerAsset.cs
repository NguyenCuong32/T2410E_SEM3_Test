namespace BattleGameFunction.Models
{
    public class PlayerAsset
{
    public int Id { get; set; }

    public int PlayerId { get; set; }
    public Player Player { get; set; }

    public int AssetId { get; set; }
    public Asset Asset { get; set; }
}
}