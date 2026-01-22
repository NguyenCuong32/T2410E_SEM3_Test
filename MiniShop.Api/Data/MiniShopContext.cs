using Microsoft.EntityFrameworkCore;
using MiniShop.Api.Models;

namespace MiniShop.Api.Data;

public class MiniShopContext : DbContext
{
    public MiniShopContext(DbContextOptions<MiniShopContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    // Keyless entity for SP result
    public DbSet<OrderWithCustomerResult> OrderWithCustomerResults { get; set; }
    public DbSet<OrderDetailResult> OrderDetailResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderWithCustomerResult>().HasNoKey().ToView(null);
        modelBuilder.Entity<OrderDetailResult>().HasNoKey().ToView(null);
    }
}

public class OrderWithCustomerResult
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
}

public class OrderDetailResult
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}
