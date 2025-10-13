using AutoMapper;
using Axolotl.Application.Dtos.ProductPriceHistory.Requests;
using Axolotl.Domain.Entities;

namespace Axolotl.Application.Mappings
{
    public class ProductStockProfile : Profile
    {
        public ProductStockProfile()
        {
            CreateMap<ProductStock, ProductStockResponse>();
        }
    }
}
