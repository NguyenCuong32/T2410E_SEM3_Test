using AutoMapper;
using MiniShop.Api.DTOs;
using MiniShop.Api.Data;
using MiniShop.Api.Models;

namespace MiniShop.Api.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product Mappings
        CreateMap<Product, ProductDto>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();

        // Order Mappings (from SP results)
        CreateMap<OrderWithCustomerResult, OrderSummaryDto>();
        CreateMap<OrderDetailResult, OrderDetailDto>();
    }
}
