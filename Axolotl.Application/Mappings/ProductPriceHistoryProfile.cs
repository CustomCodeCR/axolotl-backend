using AutoMapper;
using Axolotl.Application.Dtos.ProductPriceHistory.Requests;
using Axolotl.Domain.Entities;

namespace Axolotl.Application.Mappings
{
    public class ProductPriceHistoryProfile : Profile
    {
        public ProductPriceHistoryProfile()
        {
            CreateMap<ProductPriceHistory, ProductPriceHistoryResponse>();
        }
    }
}
