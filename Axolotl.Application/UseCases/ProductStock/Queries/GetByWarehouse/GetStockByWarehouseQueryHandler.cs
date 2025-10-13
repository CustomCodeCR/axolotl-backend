using AutoMapper;
using Axolotl.Application.Dtos.ProductPriceHistory.Requests;
using Axolotl.Application.Interfaces.Persistence;
using MediatR;

namespace Axolotl.Application.UseCases.ProductStock.Queries.GetByWarehouse
{
    public sealed class GetStockByWarehouseQueryHandler
    : IRequestHandler<GetStockByWarehouseQuery, List<ProductStockResponse>>
    {
        private readonly IProductStockRepository _repo;
        private readonly IMapper _mapper;

        public GetStockByWarehouseQueryHandler(IProductStockRepository repo, IMapper mapper)
        { _repo = repo; _mapper = mapper; }

        public async Task<List<ProductStockResponse>> Handle(GetStockByWarehouseQuery q, CancellationToken ct)
        {
            var rows = await _repo.GetByWarehouseAsync(q.WarehouseId, ct);
            return rows.Select(_mapper.Map<ProductStockResponse>).ToList();
        }
    }
}
