using AutoMapper;
using Axolotl.Application.Dtos.ProductPriceHistory.Requests;
using Axolotl.Application.Interfaces.Persistence;
using MediatR;

namespace Axolotl.Application.UseCases.ProductPriceHistory.Queries.GetCurrentByProduct
{
    public sealed class GetCurrentPriceQueryHandler
    : IRequestHandler<GetCurrentPriceQuery, ProductPriceHistoryResponse?>
    {
        private readonly IProductPriceHistoryRepository _repo;
        private readonly IMapper _mapper;

        public GetCurrentPriceQueryHandler(IProductPriceHistoryRepository repo, IMapper mapper)
        { _repo = repo; _mapper = mapper; }

        public async Task<ProductPriceHistoryResponse?> Handle(GetCurrentPriceQuery q, CancellationToken ct)
        {
            var row = await _repo.GetCurrentAsync(q.ProductId, q.At, ct);
            return row is null ? null : _mapper.Map<ProductPriceHistoryResponse>(row);
        }
    }
}
