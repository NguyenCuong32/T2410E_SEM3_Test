using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniShop.Api.Data;

namespace MiniShop.Api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly MiniShopContext _context;

    public OrderRepository(MiniShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderWithCustomerResult>> GetOrdersWithCustomerAsync()
    {
        return await _context.OrderWithCustomerResults
            .FromSqlRaw("EXEC sp_GetOrdersWithCustomers")
            .ToListAsync();
    }

    public async Task<IEnumerable<OrderDetailResult>> GetOrderDetailsAsync(int orderId)
    {
        return await _context.OrderDetailResults
            .FromSqlRaw("EXEC sp_GetOrderDetails @OrderId", new SqlParameter("@OrderId", orderId))
            .ToListAsync();
    }
}
