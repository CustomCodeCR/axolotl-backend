using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Dtos.ProductPriceHistory.Requests;
using MediatR;

namespace Axolotl.Application.UseCases.ProductPriceHistory.Commands.ChangePrice
{
    public sealed class ChangePriceCommand : IRequest<BaseResponse<bool>>
    {
        public ChangeProductPriceDto Dto { get; init; } = null!;
    }
}
