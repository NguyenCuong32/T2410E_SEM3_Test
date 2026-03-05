using Microsoft.EntityFrameworkCore;
using BattleGame.Models;

namespace BattleGame.Data
{
    public class BattleGameDbContext : DbContext   // phải là public
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<PlayerAsset> PlayerAssets { get; set; }

        public BattleGameDbContext(DbContextOptions<BattleGameDbContext> options)
            : base(options)
        {
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder.UseSqlServer("Server=tcp:...;Database=BATTLEGAME;...");
        //     }
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerAsset>()
                .HasKey(pa => new { pa.PlayerId, pa.AssetId });

            modelBuilder.Entity<PlayerAsset>()
                .HasOne(pa => pa.Player)
                .WithMany(p => p.PlayerAssets)
                .HasForeignKey(pa => pa.PlayerId);

            modelBuilder.Entity<PlayerAsset>()
                .HasOne(pa => pa.Asset)
                .WithMany(a => a.PlayerAssets)
                .HasForeignKey(pa => pa.AssetId);

            // Map columns theo schema trong ảnh
            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.PlayerId).HasColumnName("PlayerId");
                entity.Property(e => e.PlayerName).HasMaxLength(64);
                entity.Property(e => e.FullName).HasMaxLength(128);
                entity.Property(e => e.Email).HasMaxLength(64);
                entity.Property(e => e.Age);
                entity.Property(e => e.CurrentLevel).HasColumnName("Level");
            });

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.Property(e => e.AssetId).HasColumnName("AssetId");
                entity.Property(e => e.AssetName).HasMaxLength(64);
                entity.Property(e => e.LevelRequire);
                entity.Property(e => e.Description);
            });
        }
    }
}