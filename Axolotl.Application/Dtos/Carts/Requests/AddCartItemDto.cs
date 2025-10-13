namespace Axolotl.Application.Dtos.Carts.Requests;

public record AddCartItemDto(Guid CustomerId, Guid ProductId, int Quantity, decimal UnitPrice);
