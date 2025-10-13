using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Axolotl.Application.Dtos.Sales.Requests
{
    public sealed class SaleItemResponse
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal DiscountPct { get; init; }
        public decimal Subtotal => Math.Round(Quantity * UnitPrice * (1 - DiscountPct), 2);
    }
}
