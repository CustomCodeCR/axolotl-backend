using MediatR;
using Axolotl.Application.Dtos.Payments.Requests;

namespace Axolotl.Application.UseCases.Payments.Commands.UpdatePaymentStatus
{
    public sealed record UpdatePaymentStatusCommand(UpdatePaymentStatusDto Dto) : IRequest<Unit>;
}
