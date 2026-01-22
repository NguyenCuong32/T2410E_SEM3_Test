using Microsoft.EntityFrameworkCore;
using StudentManagement.Models; 
namespace StudentManagement.DBContext
{
    public class StudentManagementDBContext : DbContext
    {
        
        public StudentManagementDBContext() : base()
        {
        }

       
        public StudentManagementDBContext(DbContextOptions<StudentManagementDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Student>();
            modelBuilder.Entity<Enrollment>();
            modelBuilder.Entity<Course>();

           
        }

        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<Course> Courses { get; set; } = default!;
        public DbSet<Enrollment> Enrollments { get; set; } = default!;
    }
}