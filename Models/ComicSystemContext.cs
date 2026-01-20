using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class ComicSystemContext : DbContext
{
    public ComicSystemContext(DbContextOptions options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<ComicBook> ComicBooks { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<RentalDetail> RentalDetails { get; set; }
}
