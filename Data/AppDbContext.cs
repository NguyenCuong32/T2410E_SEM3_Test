using Microsoft.EntityFrameworkCore;
using T2410E_SEM3_Test.Models;

namespace T2410E_SEM3_Test.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Player> Players { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<PlayerAsset> PlayerAssets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerAsset>()
            .HasKey(pa => new { pa.PlayerId, pa.AssetId });

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

        modelBuilder.Entity<Player>()
            .Property(p => p.PlayerId)
            .HasDefaultValueSql("NEWID()");

        modelBuilder.Entity<Player>()
            .Property(p => p.Level)
            .HasDefaultValue(1);

        modelBuilder.Entity<Asset>()
            .Property(a => a.AssetId)
            .HasDefaultValueSql("NEWID()");

        modelBuilder.Entity<Asset>()
            .Property(a => a.LevelRequire)
            .HasDefaultValue(0);

        base.OnModelCreating(modelBuilder);
    }
}
