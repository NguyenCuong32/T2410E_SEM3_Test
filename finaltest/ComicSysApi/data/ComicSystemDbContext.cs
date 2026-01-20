using ComicSysApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicSysApi.Data;

public class ComicSystemDbContext : DbContext
{
    public ComicSystemDbContext(DbContextOptions<ComicSystemDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<ComicBook> ComicBooks => Set<ComicBook>();
    public DbSet<Rental> Rentals => Set<Rental>();
    public DbSet<RentalDetail> RentalDetails => Set<RentalDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Rentals)
            .HasForeignKey(r => r.CustomerID)
            .OnDelete(DeleteBehavior.Restrict);

       
        modelBuilder.Entity<RentalDetail>()
            .HasOne(d => d.Rental)
            .WithMany(r => r.RentalDetails)
            .HasForeignKey(d => d.RentalID)
            .OnDelete(DeleteBehavior.Cascade);

        
        modelBuilder.Entity<RentalDetail>()
            .HasOne(d => d.ComicBook)
            .WithMany(b => b.RentalDetails)
            .HasForeignKey(d => d.ComicBookID)
            .OnDelete(DeleteBehavior.Restrict);

        
        modelBuilder.Entity<ComicBook>()
            .Property(b => b.PricePerDay)
            .HasPrecision(10, 2);

        modelBuilder.Entity<RentalDetail>()
            .Property(d => d.PricePerDay)
            .HasPrecision(10, 2);

        base.OnModelCreating(modelBuilder);
    }
}
