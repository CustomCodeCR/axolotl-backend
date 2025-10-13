namespace Axolotl.Application.Dtos.Orders.Requests;

public class CreateOrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPct { get; set; }
}

public class CreateOrderDto
{
    public Guid CustomerId { get; set; }
    public Guid WarehouseId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public string? Notes { get; set; }
    public IEnumerable<CreateOrderItemDto> Items { get; set; } = new List<CreateOrderItemDto>();
}
