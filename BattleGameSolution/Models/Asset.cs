using System;

namespace BattleGameSolution.Models
{
    public class Asset
    {
        public Guid AssetId { get; set; }

        public string AssetName { get; set; }

        public int LevelRequire { get; set; }
    }
}