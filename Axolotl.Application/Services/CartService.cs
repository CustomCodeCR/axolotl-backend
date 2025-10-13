using Axolotl.Application.Dtos.Carts;
using Axolotl.Application.Dtos.Carts.Requests;
using Axolotl.Application.Interfaces.Persistence;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Domain.Entities;
using Axolotl.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _repo;
    private readonly IUnitOfWork _uow;

    public CartService(ICartRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<CartResponse> GetAsync(Guid customerId, CancellationToken ct)
    {
        var cart = await _repo.GetByCustomerAsync(customerId, ct)
                   ?? new Carts { CustomerId = customerId, Status = CartStatus.OPEN };

        if (cart.UUID == Guid.Empty)
        {
            await _repo.CreateAsync(cart);
            await _uow.SaveChangesAsync(ct);
        }

        return new CartResponse(
            cart.UUID,
            cart.CustomerId,
            cart.CartItems.Select(i => new CartItemResponse(i.ProductId, i.Quantity, i.UnitPrice)).ToList()
        );
    }

    public async Task AddItemAsync(AddCartItemDto dto, CancellationToken ct)
    {
        var cart = await _repo.GetByCustomerAsync(dto.CustomerId, ct)
                   ?? new Carts { CustomerId = dto.CustomerId, Status = CartStatus.OPEN };

        var existing = cart.CartItems.FirstOrDefault(i => i.ProductId == dto.ProductId);
        if (existing is null)
            cart.CartItems.Add(new CartItems { ProductId = dto.ProductId, Quantity = dto.Quantity, UnitPrice = dto.UnitPrice });
        else
            existing.Quantity += dto.Quantity;

        if (cart.UUID == Guid.Empty) await _repo.CreateAsync(cart); else _repo.UpdateAsync(cart);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task UpdateQtyAsync(Guid customerId, Guid productId, int quantity, CancellationToken ct)
    {
        var cart = await _repo.GetByCustomerAsync(customerId, ct) ?? throw new KeyNotFoundException("Cart not found");
        var item = cart.CartItems.FirstOrDefault(i => i.ProductId == productId) ?? throw new KeyNotFoundException("Item not found");
        item.Quantity = quantity;
        _repo.UpdateAsync(cart);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task RemoveItemAsync(Guid customerId, Guid productId, CancellationToken ct)
    {
        var cart = await _repo.GetByCustomerAsync(customerId, ct) ?? throw new KeyNotFoundException("Cart not found");
        cart.CartItems = cart.CartItems.Where(i => i.ProductId != productId).ToList();
        _repo.UpdateAsync(cart);
        await _uow.SaveChangesAsync(ct);
    }

    public async Task ClearAsync(Guid customerId, CancellationToken ct)
    {
        var cart = await _repo.GetByCustomerAsync(customerId, ct) ?? throw new KeyNotFoundException("Cart not found");
        cart.CartItems.Clear();
        _repo.UpdateAsync(cart);
        await _uow.SaveChangesAsync(ct);
    }
}
