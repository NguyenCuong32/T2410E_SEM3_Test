using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Api.DTOs;
using MiniShop.Api.Repositories;

namespace MiniShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public OrdersController(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderSummaryDto>>> GetAll()
    {
        var orders = await _repository.GetOrdersWithCustomerAsync();
        return Ok(_mapper.Map<IEnumerable<OrderSummaryDto>>(orders));
    }

    [HttpGet("{id}/details")]
    public async Task<ActionResult<IEnumerable<OrderDetailDto>>> GetDetails(int id)
    {
        var details = await _repository.GetOrderDetailsAsync(id);
        return Ok(_mapper.Map<IEnumerable<OrderDetailDto>>(details));
    }
}
