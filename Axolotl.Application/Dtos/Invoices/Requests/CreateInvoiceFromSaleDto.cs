namespace Axolotl.Application.Dtos.Invoices.Requests
{
    public sealed class CreateInvoiceFromSaleDto
    {
        public Guid SaleId { get; init; }
        public Guid CustomerId { get; init; }
        public Guid BillingAddressId { get; init; }
        public DateTime? DueDate { get; init; } // si null => SaleDate + 30d (o DateTime.UtcNow + 30d)
        public string? Notes { get; init; }
    }
}
