using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axolotl.Application.Dtos.Payments.Requests
{
    public sealed class CreatePaymentDto
{
    public Guid InvoiceId { get; init; }
    public Guid PaymentMethodId { get; init; }
    public decimal Amount { get; init; }
    public DateTime? PaymentDate { get; init; } 
    public string? Reference { get; init; }
}
}
