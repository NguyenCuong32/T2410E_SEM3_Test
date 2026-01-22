using MiniShop.Api.Models;

namespace MiniShop.Api.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
}
