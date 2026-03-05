using Microsoft.EntityFrameworkCore;
using BattleGameFunction.Models;

namespace BattleGameFunction.Data
{
    public class BattleGameDbContext : DbContext
    {
        public BattleGameDbContext(DbContextOptions<BattleGameDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<PlayerAsset> PlayerAssets { get; set; }
    }
}