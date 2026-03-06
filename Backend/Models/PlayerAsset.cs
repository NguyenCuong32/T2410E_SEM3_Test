using System;

namespace Backend.Models
{
    public class PlayerAsset
    {
        public Guid PlayerId { get; set; }
        public Guid AssetId { get; set; }
    }
}
