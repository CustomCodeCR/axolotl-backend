namespace Axolotl.Domain.Entities;

public class SalesReturns : BaseEntity
{
    public Guid InvoiceId { get; set; }
    public DateTime ReturnDate { get; set; }
    public string? Reason { get; set; }

    public virtual Invoices Invoices { get; set; } = null!;
    public virtual ICollection<SalesReturnItems> SalesReturnItems { get; set; } = new List<SalesReturnItems>();
}