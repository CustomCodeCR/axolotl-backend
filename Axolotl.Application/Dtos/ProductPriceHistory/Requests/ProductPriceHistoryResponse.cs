namespace Axolotl.Application.Dtos.ProductPriceHistory.Requests
{
    public sealed class ProductPriceHistoryResponse
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public DateTime ValidFrom { get; init; }
        public DateTime ValidTo { get; init; }
        public decimal Price { get; init; }
        public decimal Cost { get; init; }
        public string? Note { get; init; }
    }
}
