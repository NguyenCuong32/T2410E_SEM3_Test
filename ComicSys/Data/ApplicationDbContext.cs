using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<ComicBook> ComicBooks { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<RentalDetail> RentalDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ComicBook>()
            .Property(c => c.PricePerDay)
            .HasPrecision(10, 2);

        modelBuilder.Entity<RentalDetail>()
            .Property(r => r.PricePerDay)
            .HasPrecision(10, 2);
    }
}
