using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniShop.Api.Data;
using MiniShop.Api.Models;

namespace MiniShop.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MiniShopContext _context;

    public ProductRepository(MiniShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
    {
        return await _context.Products
            .FromSqlRaw("EXEC sp_GetProductsByCategory @CategoryId", new SqlParameter("@CategoryId", categoryId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .FromSqlRaw("EXEC sp_GetAllProducts")
            .ToListAsync();
    }

    public async Task CreateAsync(Product product)
    {
        var pName = new SqlParameter("@Name", product.Name);
        var pPrice = new SqlParameter("@Price", product.Price);
        var pCatId = new SqlParameter("@CategoryId", product.CategoryId);

        await _context.Database.ExecuteSqlRawAsync("EXEC sp_CreateProduct @Name, @Price, @CategoryId", pName, pPrice, pCatId);
    }

    public async Task UpdateAsync(Product product)
    {
        var pId = new SqlParameter("@Id", product.Id);
        var pName = new SqlParameter("@Name", product.Name);
        var pPrice = new SqlParameter("@Price", product.Price);
        var pCatId = new SqlParameter("@CategoryId", product.CategoryId);

        await _context.Database.ExecuteSqlRawAsync("EXEC sp_UpdateProduct @Id, @Name, @Price, @CategoryId", pId, pName, pPrice, pCatId);
    }

    public async Task DeleteAsync(int id)
    {
        await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteProduct @Id", new SqlParameter("@Id", id));
    }
    
    public async Task<Product?> GetByIdAsync(int id)
    {
        // Not specifically asked for in SP list, but useful. Using standard EF for single fetch or added SP if needed.
        // For strict SP Project:
        // Adding a simple query or assuming standard EF for simple gets is OK if not restricted.
        // But user asked: "API cần gọi Stored Procedure để thêm mới, sửa, xoá, lấy dữ liệu"
        // I will use standard EF FindAsync for basic ID check or assume we need to add SP for GetById if strictly required.
        // For now, standard EF is acceptable for simple ID lookup, or I can add sp_GetProductById.
        // Keeping it simple with existing SPs first.
        var result = await _context.Products
            .FromSqlRaw("EXEC sp_GetProductById @Id", new SqlParameter("@Id", id))
            .ToListAsync();
        return result.FirstOrDefault();
    }
}
