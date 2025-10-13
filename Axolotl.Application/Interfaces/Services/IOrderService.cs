using Axolotl.Application.Dtos.Orders;
using Axolotl.Application.Dtos.Orders.Requests;

namespace Axolotl.Application.Interfaces.Services;

public interface IOrderService
{
    Task<Guid> CreateAsync(CreateOrderDto dto, CancellationToken ct);
    Task<IReadOnlyList<OrderListResponse>> ListAsync(CancellationToken ct);
    Task<OrderDetailResponse?> GetByIdAsync(Guid uuid, CancellationToken ct);
}
