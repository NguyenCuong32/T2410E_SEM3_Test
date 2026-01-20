using Microsoft.EntityFrameworkCore;

namespace ASM.Models
{
    public class ASMContext : DbContext
    {
        public ASMContext(DbContextOptions<ASMContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }
    }
}
