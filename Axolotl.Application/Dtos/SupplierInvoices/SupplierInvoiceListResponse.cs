
namespace Axolotl.Application.Dtos.SupplierInvoices;

public record SupplierInvoiceListResponse(Guid Id, string InvoiceNumber, decimal TotalIncTax);

public record SupplierInvoiceDetailItem(Guid ProductId, int Quantity, decimal UnitCost, decimal DiscountPct, string? Description);

public record SupplierInvoiceDetailResponse(
    Guid Id,
    string InvoiceNumber,
    decimal TotalExTax,
    decimal TotalTax,
    decimal TotalIncTax,
    IReadOnlyList<SupplierInvoiceDetailItem> Items
);
