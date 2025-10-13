using AutoMapper;
using Axolotl.Application.Dtos.ProductPriceHistory.Requests;
using Axolotl.Application.Interfaces.Persistence;
using MediatR;

namespace Axolotl.Application.UseCases.ProductPriceHistory.Queries.GetAllByProduct
{
    public sealed class GetAllByProductQueryHandler
        : IRequestHandler<GetAllByProductQuery, List<ProductPriceHistoryResponse>>
    {
        private readonly IProductPriceHistoryRepository _repo;
        private readonly IMapper _mapper;

        public GetAllByProductQueryHandler(IProductPriceHistoryRepository repo, IMapper mapper)
        { _repo = repo; _mapper = mapper; }

        public async Task<List<ProductPriceHistoryResponse>> Handle(GetAllByProductQuery q, CancellationToken ct)
        {
            var list = await _repo.GetByProductAsync(q.ProductId, ct);
            return list.Select(_mapper.Map<ProductPriceHistoryResponse>).ToList();
        }
    }
}
