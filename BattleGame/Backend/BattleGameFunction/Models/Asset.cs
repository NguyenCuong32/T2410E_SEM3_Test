namespace BattleGameFunction.Models
{
    public class Asset
{
    public int Id { get; set; }
    public string AssetName { get; set; }
    public string Description { get; set; }

    public ICollection<PlayerAsset> PlayerAssets { get; set; }
}
}