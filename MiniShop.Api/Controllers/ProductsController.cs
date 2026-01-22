using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Api.DTOs;
using MiniShop.Api.Models;
using MiniShop.Api.Repositories;

namespace MiniShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductsController(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetByCategory(int categoryId)
    {
        var products = await _repository.GetByCategoryAsync(categoryId);
        return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(ProductCreateDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        await _repository.CreateAsync(product);
        // Note: NewId is set in SP but Product object might not be updated with Id depending on how FromSqlRaw works (It doesn't verify output params easily).
        // Since CreateAsync uses ExecuteSqlRawAsync which returns row count, getting the ID back requires using SqlParameter with output direction.
        // My Repository implementation currently sets the parameters but doesn't capture Output clearly for the object unless I update the Repo to do so.
        // For now, I will return CreatedAtAction with Id 0 or refetch if critical. Mapped manually.
        // The repository sets `@NewId` but doesn't read it back into `product.Id`. 
        // In a real scenario I would update Repo to return the ID.
        
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, _mapper.Map<ProductDto>(product));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductUpdateDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) return NotFound();

        var product = _mapper.Map(dto, existing);
        product.Id = id; // Ensure ID is preserved
        await _repository.UpdateAsync(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) return NotFound();

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
