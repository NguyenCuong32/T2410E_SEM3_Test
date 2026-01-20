using ComicSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Data;

public class ComicSystemContext : DbContext
{
    public ComicSystemContext(DbContextOptions<ComicSystemContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<ComicBook> ComicBooks => Set<ComicBook>();
    public DbSet<Rental> Rentals => Set<Rental>();
    public DbSet<RentalDetail> RentalDetails => Set<RentalDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Rentals)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RentalDetail>()
            .HasOne(d => d.Rental)
            .WithMany(r => r.Details)
            .HasForeignKey(d => d.RentalId);

        modelBuilder.Entity<RentalDetail>()
            .HasOne(d => d.ComicBook)
            .WithMany(b => b.RentalDetails)
            .HasForeignKey(d => d.ComicBookId);

        base.OnModelCreating(modelBuilder);
    }
}
