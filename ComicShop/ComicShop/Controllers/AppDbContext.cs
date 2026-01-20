using ComicShop.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicShop.Controllers;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Customer> customers { get; set; }
    public DbSet<ComicBook> comicbooks { get; set; }
    public DbSet<Rental> rentals { get; set; }
    public DbSet<RentalDetail> rentaldetails { get; set; }
}