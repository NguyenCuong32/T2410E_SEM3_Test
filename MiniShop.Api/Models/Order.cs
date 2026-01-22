using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniShop.Api.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
}
