using Comic_Book_Rental.Models;
using Microsoft.EntityFrameworkCore;

namespace Comic_Book_Rental.Data
{
    public class ComicSystemContext : DbContext
    {
        public ComicSystemContext(DbContextOptions<ComicSystemContext> options) : base(options)
        {
        }

        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }

        // Code này để định nghĩa bảng nào là bảng nhiều - nhiều (nếu cần)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComicBook>().ToTable("ComicBooks");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Rental>().ToTable("Rentals");
            modelBuilder.Entity<RentalDetail>().ToTable("RentalDetails");
        }
    }
}