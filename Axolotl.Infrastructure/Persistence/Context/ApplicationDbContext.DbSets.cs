using Axolotl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Axolotl.Infrastructure.Persistence.Context;

// Debe ser EXACTAMENTE el mismo namespace y nombre de clase que tu ApplicationDbContext
public partial class ApplicationDbContext : DbContext
{
	public DbSet<InventoryMovements> InventoryMovements => Set<InventoryMovements>();
	public DbSet<Orders> Orders => Set<Orders>();
	public DbSet<OrderItems> OrderItems => Set<OrderItems>();

    public DbSet<PurchaseOrders> PurchaseOrders => Set<PurchaseOrders>();
    public DbSet<PurchaseOrderItems> PurchaseOrderItems => Set<PurchaseOrderItems>();

    public DbSet<SupplierInvoices> SupplierInvoices => Set<SupplierInvoices>();
    public DbSet<SupplierInvoiceItems> SupplierInvoiceItems => Set<SupplierInvoiceItems>();

    public DbSet<Carts> Carts => Set<Carts>();
    public DbSet<CartItems> CartItems => Set<CartItems>();
}
