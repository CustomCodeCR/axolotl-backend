using Axolotl.Application.Dtos.Carts;
using Axolotl.Application.Dtos.Carts.Requests;

namespace Axolotl.Application.Interfaces.Services;

public interface ICartService
{
    Task<CartResponse> GetAsync(Guid customerId, CancellationToken ct);
    Task AddItemAsync(AddCartItemDto dto, CancellationToken ct);
    Task UpdateQtyAsync(Guid customerId, Guid productId, int quantity, CancellationToken ct);
    Task RemoveItemAsync(Guid customerId, Guid productId, CancellationToken ct);
    Task ClearAsync(Guid customerId, CancellationToken ct);
}
