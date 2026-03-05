using Microsoft.EntityFrameworkCore;
using BAITHI1.Models;

namespace BAITHI1.Data
{
    public class BattleGameContext : DbContext
    {
        public BattleGameContext(DbContextOptions<BattleGameContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<PlayerAsset> PlayerAssets { get; set; }
    }
}