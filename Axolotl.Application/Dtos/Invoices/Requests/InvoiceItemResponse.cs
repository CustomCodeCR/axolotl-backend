namespace Axolotl.Application.Dtos.Invoices.Requests
{
    public sealed class InvoiceItemResponse
    {
        public Guid UUID { get; init; }
        public Guid ProductId { get; init; }
        public string? Description { get; init; }
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal DiscountPct { get; init; }
        public decimal LineTotal => Math.Round(Quantity * UnitPrice * (1 - DiscountPct), 2);
    }
}
