namespace Axolotl.Application.Dtos.Invoices.Requests
{
    public sealed class InvoiceResponse
    {
        public Guid UUID { get; init; }
        public string InvoiceNumber { get; init; } = null!;
        public Guid SaleId { get; init; }
        public Guid CustomerId { get; init; }
        public DateTime DueDate { get; init; }
        public string Status { get; init; } = null!;
        public decimal TotalExTax { get; init; }
        public decimal TotalTax { get; init; }
        public decimal TotalIncTax { get; init; }
        public List<InvoiceItemResponse> Items { get; init; } = new();
    }
}
