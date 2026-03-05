using BattleGame.Functions.Models;
using Microsoft.EntityFrameworkCore;

namespace BattleGame.Functions.Data;

public class BattleGameDbContext : DbContext
{
    public BattleGameDbContext(DbContextOptions<BattleGameDbContext> options) : base(options) { }

    public DbSet<Player> Players { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<PlayerAsset> PlayerAssets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerAsset>()
            .HasOne(pa => pa.Player)
            .WithMany(p => p.PlayerAssets)
            .HasForeignKey(pa => pa.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PlayerAsset>()
            .HasOne(pa => pa.Asset)
            .WithMany(a => a.PlayerAssets)
            .HasForeignKey(pa => pa.AssetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
