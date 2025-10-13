using MediatR;
using Axolotl.Application.Dtos.Invoices.Requests;

namespace Axolotl.Application.UseCases.Invoices.Commands.CreateFromSale
{
    public sealed record CreateInvoiceFromSaleCommand(CreateInvoiceFromSaleDto Dto) : IRequest<Guid>;
}
