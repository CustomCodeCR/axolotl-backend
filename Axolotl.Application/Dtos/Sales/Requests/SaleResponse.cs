using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axolotl.Application.Dtos.Sales.Requests
{
    public sealed class SaleResponse
    {
        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public Guid WarehouseId { get; init; }
        public DateTime SaleDate { get; init; }
        public string? Notes { get; init; }
        public List<SaleItemResponse> Items { get; init; } = new();
        public decimal Total => Items.Sum(i => i.Subtotal);
    }
}
