using System;

namespace BattleGame.Models
{
    public class PlayerAsset
    {
        public Guid PlayerId { get; set; }
        public Player? Player { get; set; }  // Nullable (navigation prop, EF handle)

        public Guid AssetId { get; set; }
        public Asset? Asset { get; set; }  // Nullable (navigation prop, EF handle)
    }
}