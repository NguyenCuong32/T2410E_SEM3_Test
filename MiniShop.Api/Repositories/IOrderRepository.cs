using MiniShop.Api.Data;
using MiniShop.Api.Models;

namespace MiniShop.Api.Repositories;

public interface IOrderRepository
{
    Task<IEnumerable<OrderWithCustomerResult>> GetOrdersWithCustomerAsync();
    Task<IEnumerable<OrderDetailResult>> GetOrderDetailsAsync(int orderId);
}
