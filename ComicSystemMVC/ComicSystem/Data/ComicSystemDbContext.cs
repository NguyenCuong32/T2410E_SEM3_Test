using Microsoft.EntityFrameworkCore;
using ComicSystem.Models;

namespace ComicSystem.Data
{
    public class ComicSystemDbContext : DbContext
    {
        public ComicSystemDbContext(DbContextOptions<ComicSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RentalDetail>()
                .HasOne(rd => rd.Rental)
                .WithMany(r => r.RentalDetails)
                .HasForeignKey(rd => rd.RentalID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RentalDetail>()
                .HasOne(rd => rd.ComicBook)
                .WithMany(cb => cb.RentalDetails)
                .HasForeignKey(rd => rd.ComicBookID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
