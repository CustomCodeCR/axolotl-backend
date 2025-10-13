using Axolotl.Application.Dtos.Orders;
using Axolotl.Application.Dtos.Orders.Requests;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Domain.Entities;
using Axolotl.Domain.Enums;

namespace Axolotl.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orders;
    private readonly IUnitOfWork _uow;

    public OrderService(IOrderRepository orders, IUnitOfWork uow)
    {
        _orders = orders; _uow = uow;
    }

    public async Task<Guid> CreateAsync(CreateOrderDto dto, CancellationToken ct)
    {
        var order = new Orders
        {
            CustomerId = dto.CustomerId,
            WarehouseId = dto.WarehouseId,
            OrderDate = dto.OrderDate,
            RequiredDate = dto.RequiredDate,
            Notes = dto.Notes,
            Status = OrderStatus.DRAFT
        };

        foreach (var i in dto.Items)
        {
            order.OrderItems.Add(new OrderItems
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                DiscountPct = i.DiscountPct
            });
        }

        await _orders.CreateAsync(order);
        await _uow.SaveChangesAsync(ct);
        return order.UUID;
    }

    public async Task<IReadOnlyList<OrderListResponse>> ListAsync(CancellationToken ct)
    {
        var data = await _orders.ListWithItemsAsync(ct);
        return data.Select(o => new OrderListResponse
        {
            Id = o.UUID,
            Status = o.Status.ToString(),
            OrderDate = o.OrderDate,
            ItemsCount = o.OrderItems.Count,
            TotalAmount = o.OrderItems.Sum(x => x.UnitPrice * x.Quantity * (1 - (x.DiscountPct / 100m)))
        }).ToList();
    }

    public async Task<OrderDetailResponse?> GetByIdAsync(Guid uuid, CancellationToken ct)
    {
        var o = await _orders.GetWithItemsAsync(uuid, ct);
        if (o is null) return null;

        return new OrderDetailResponse
        {
            Id = o.UUID,
            Status = o.Status.ToString(),
            OrderDate = o.OrderDate,
            RequiredDate = o.RequiredDate,
            Notes = o.Notes,
            TotalAmount = o.OrderItems.Sum(x => x.UnitPrice * x.Quantity * (1 - (x.DiscountPct / 100m))),
            Items = o.OrderItems.Select(i => new OrderDetailItemResponse
            {
                Id = i.UUID,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                DiscountPct = i.DiscountPct,
                LineTotal = i.UnitPrice * i.Quantity * (1 - (i.DiscountPct / 100m))
            }).ToList()
        };
    }
}
