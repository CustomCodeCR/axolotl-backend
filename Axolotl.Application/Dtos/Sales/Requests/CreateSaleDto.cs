using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axolotl.Application.Dtos.Sales.Requests
{
    public sealed class CreateSaleDto
    {
        public Guid CustomerId { get; init; }
        public Guid WarehouseId { get; init; }
        public DateTime? SaleDate { get; init; } // si es null => DateTime.UtcNow
        public string? Notes { get; init; }
        public List<CreateSaleItemDto> Items { get; init; } = new();
    }

    public sealed class CreateSaleItemDto
    {
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public decimal DiscountPct { get; init; }
    }
}
