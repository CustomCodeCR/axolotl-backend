namespace Axolotl.Application.Dtos.Carts;

public record CartItemResponse(Guid ProductId, int Quantity, decimal UnitPrice);

public record CartResponse(Guid Id, Guid CustomerId, IReadOnlyList<CartItemResponse> Items);
