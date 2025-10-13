using Axolotl.Domain.Entities;
using Axolotl.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Reflection;

namespace Axolotl.Infrastructure.Persistence.Context
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // If you need a connection, use the one EF already knows about:
        public NpgsqlConnection CreateConnection => (NpgsqlConnection)Database.GetDbConnection();

        public DbSet<Customers> Customers { get; set; }
        public DbSet<CustomerAddresses> CustomerAddresses { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<TaxRates> TaxRates { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductPriceHistory> ProductPriceHistory { get; set; }
        public DbSet<Warehouses> Warehouses { get; set; }
        public DbSet<ProductStock> ProductStock => Set<ProductStock>();
        public DbSet<InventoryMovements> InventoryMovements { get; set; }
        public DbSet<PurchaseOrders> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItems> PurchaseOrderItems { get; set; }
        public DbSet<SupplierInvoices> SupplierInvoices { get; set; }
        public DbSet<SupplierInvoiceItems> SupplierInvoiceItems { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<SaleItems> SaleItems { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<InvoiceItems> InvoiceItems { get; set; }
        public DbSet<PaymentMethods> PaymentMethods { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<SalesReturns> SalesReturns { get; set; }
        public DbSet<SalesReturnItems> SalesReturnItems { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<InventoryMovementType>("public", "inventory_movement_type");
            modelBuilder.HasPostgresEnum<PurchaseStatus>("public", "purchase_status");
            modelBuilder.HasPostgresEnum<OrderStatus>("public", "order_status");
            modelBuilder.HasPostgresEnum<SaleStatus>("public", "sale_status");
            modelBuilder.HasPostgresEnum<InvoiceStatus>("public", "invoice_status");
            modelBuilder.HasPostgresEnum<PaymentStatus>("public", "payment_status");
            modelBuilder.HasPostgresEnum<CartStatus>("public", "cart_status");
            modelBuilder.HasPostgresEnum<StateType>("public", "state_type");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}