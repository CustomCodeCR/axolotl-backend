namespace Axolotl.Application.Dtos.SupplierInvoices.Requests;

public record CreateSupplierInvoiceItemDto(Guid ProductId, int Quantity, decimal UnitCost, decimal DiscountPct, string? Description);

public record CreateSupplierInvoiceDto(
    Guid SupplierId,
    Guid PurchaseOrderId,
    string InvoiceNumber,
    List<CreateSupplierInvoiceItemDto> Items
);
