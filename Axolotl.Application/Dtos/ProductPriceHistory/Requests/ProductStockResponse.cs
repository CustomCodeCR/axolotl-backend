namespace Axolotl.Application.Dtos.ProductPriceHistory.Requests
{
    public sealed class ProductStockResponse
    {
        public Guid Id { get; init; }
        public Guid WarehouseId { get; init; }
        public Guid ProductId { get; init; }
        public int QuantityOnHand { get; init; }
        public int QuantityOnReserved { get; init; }
    }
}
