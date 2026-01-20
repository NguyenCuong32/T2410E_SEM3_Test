using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Models
{
    public class ComicSystemContext : DbContext
    {
        public ComicSystemContext(DbContextOptions<ComicSystemContext> options)
            : base(options)
        {
        }

        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RentalDetail>()
                .HasOne(rd => rd.Rental)
                .WithMany(r => r.RentalDetails)
                .HasForeignKey(rd => rd.RentalId);

            modelBuilder.Entity<RentalDetail>()
                .HasOne(rd => rd.ComicBook)
                .WithMany()
                .HasForeignKey(rd => rd.ComicBookId);
        }
    }
}
