using System;

namespace BattleGameFunctionAPI.Models
{
    public class PlayerAsset
    {
        public Guid PlayerId { get; set; }

        public Guid AssetId { get; set; }
    }
}