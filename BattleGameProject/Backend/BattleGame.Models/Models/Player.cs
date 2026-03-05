using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleGame.Models
{
    public class Player
    {
        [Key]
        public Guid PlayerId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(64)]
        public required string PlayerName { get; set; }  // Required theo schema

        [MaxLength(128)]
        public string? FullName { get; set; }  // Nullable (optional)

        [MaxLength(64)]
        public string? Email { get; set; }  // Nullable (optional)

        public int Age { get; set; }

        public int CurrentLevel { get; set; }

        public ICollection<PlayerAsset> PlayerAssets { get; set; } = new List<PlayerAsset>();
    }
}