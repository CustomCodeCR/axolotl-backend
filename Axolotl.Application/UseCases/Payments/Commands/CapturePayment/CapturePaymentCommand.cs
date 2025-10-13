using MediatR;
using Axolotl.Application.Dtos.Payments.Requests;

namespace Axolotl.Application.UseCases.Payments.Commands.CapturePayment
{
    public sealed record CapturePaymentCommand(CreatePaymentDto Dto) : IRequest<Guid>;
}
