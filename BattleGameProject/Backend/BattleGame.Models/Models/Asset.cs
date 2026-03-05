using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleGame.Models
{
    public class Asset
    {
        [Key]
        public Guid AssetId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(64)]
        public required string AssetName { get; set; }  // Required theo schema

        public int LevelRequire { get; set; }

        public string? Description { get; set; }  // Nullable (schema nvarchar no length, optional)

        public ICollection<PlayerAsset> PlayerAssets { get; set; } = new List<PlayerAsset>();
    }
}