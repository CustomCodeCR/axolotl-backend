namespace Axolotl.Domain.Entities;

public class CustomerAddresses : BaseEntity
{
    public string CustomerId { get;  set; } = null!;
    public string Label { get; set; } = null!;
    public string Address1 { get; set; } = null!;
    public string? Address2 { get; set; }
    public string Province { get; set; } = null!;
    public string Canton { get; set; } = null!;
    public string District { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Country { get; set; } = null!;
    public bool IsDefault { get; set; } = false;

    public virtual Customers Customers { get; set; } = null!;
}