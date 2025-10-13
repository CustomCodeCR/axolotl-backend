namespace Axolotl.Application.Dtos.Orders;

public class OrderListResponse
{
    public Guid Id { get; set; }           
    public string Status { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public int ItemsCount { get; set; }
    public decimal TotalAmount { get; set; }
}

public class OrderDetailItemResponse
{
    public Guid Id { get; set; }           
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountPct { get; set; }
    public decimal LineTotal { get; set; }
}

public class OrderDetailResponse
{
    public Guid Id { get; set; }           
    public string Status { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public DateTime RequiredDate { get; set; }
    public string? Notes { get; set; }
    public decimal TotalAmount { get; set; }
    public IEnumerable<OrderDetailItemResponse> Items { get; set; } = new List<OrderDetailItemResponse>();
}
