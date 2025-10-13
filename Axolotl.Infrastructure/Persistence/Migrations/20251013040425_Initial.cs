using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Axolotl.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:public.cart_status", "open,checked_out,abandoned")
                .Annotation("Npgsql:Enum:public.inventory_movement_type", "purchase_in,purchase_return_out,sale_out,sale_return_in,adjustment_in,adjustment_out,transfer_in,transfer_out")
                .Annotation("Npgsql:Enum:public.invoice_status", "draft,issued,paid,cancelled")
                .Annotation("Npgsql:Enum:public.order_status", "draft,confirmed,fulfilled,invoiced,cancelled")
                .Annotation("Npgsql:Enum:public.payment_status", "pending,completed,failed,reversed")
                .Annotation("Npgsql:Enum:public.purchase_status", "draft,approved,received,invoiced,cancelled")
                .Annotation("Npgsql:Enum:public.sale_status", "open,closed,void")
                .Annotation("Npgsql:Enum:public.state_type", "inactive,active");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "text", unicode: false, nullable: true),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ID = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ID = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    HireDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    ID = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    ContactName = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    TaxRateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    RatePercent = table.Column<decimal>(type: "numeric(7,3)", precision: 7, scale: 3, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.TaxRateId);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    UnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(16)", unicode: false, maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.UnitId);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", unicode: false, maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    Province = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Canton = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Distric = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.WarehouseId);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "cart_status", nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Carts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddresses",
                columns: table => new
                {
                    CustomerAddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Address1 = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    Address2 = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: true),
                    Province = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Canton = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    District = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    Country = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddresses", x => x.CustomerAddressId);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    SKU = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", unicode: false, nullable: true),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnitId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaxRateId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_TaxRates_TaxRateId",
                        column: x => x.TaxRateId,
                        principalTable: "TaxRates",
                        principalColumn: "TaxRateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "order_status", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RequiredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", unicode: false, nullable: true),
                    WarehousesUUID = table.Column<Guid>(type: "uuid", nullable: true),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Warehouses_WarehousesUUID",
                        column: x => x.WarehousesUUID,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    PurchaseOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "purchase_status", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpectedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", unicode: false, nullable: true),
                    WarehousesUUID = table.Column<Guid>(type: "uuid", nullable: true),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.PurchaseOrderId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Warehouses_WarehousesUUID",
                        column: x => x.WarehousesUUID,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId");
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "sale_status", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", unicode: false, nullable: true),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                    table.ForeignKey(
                        name: "FK_Sales_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    CartId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryMovements",
                columns: table => new
                {
                    InventoryMovementId = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    MovementType = table.Column<int>(type: "inventory_movement_type", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    ReferenceTable = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    ReferenceId = table.Column<string>(type: "character varying(60)", unicode: false, maxLength: 60, nullable: false),
                    Reason = table.Column<string>(type: "text", unicode: false, nullable: true),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryMovements", x => x.InventoryMovementId);
                    table.ForeignKey(
                        name: "FK_InventoryMovements_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryMovements_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPriceHistory",
                columns: table => new
                {
                    ProductPriceHistoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: false),
                    Note = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: true),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPriceHistory", x => x.ProductPriceHistoryId);
                    table.ForeignKey(
                        name: "FK_ProductPriceHistory_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductStock",
                columns: table => new
                {
                    ProductStockId = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuantityOnHand = table.Column<int>(type: "integer", nullable: false),
                    QuantityReserved = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStock", x => x.ProductStockId);
                    table.ForeignKey(
                        name: "FK_ProductStock_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductStock_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: false),
                    DiscountPct = table.Column<decimal>(type: "numeric(7,3)", precision: 7, scale: 3, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderItems",
                columns: table => new
                {
                    PurchaseOrderItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitCost = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderItems", x => x.PurchaseOrderItemId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierInvoices",
                columns: table => new
                {
                    SupplierInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    TotalExTax = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: false),
                    TotalTax = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: false),
                    TotalIncTax = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierInvoices", x => x.SupplierInvoiceId);
                    table.ForeignKey(
                        name: "FK_SupplierInvoices_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierInvoices_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    SaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "invoice_status", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BillingAddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalExTax = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: false),
                    TotalTax = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: false),
                    TotalIncTax = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_CustomerAddresses_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "CustomerAddresses",
                        principalColumn: "CustomerAddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleItems",
                columns: table => new
                {
                    SaleItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    SaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: false),
                    DiscountPct = table.Column<decimal>(type: "numeric(7,3)", precision: 7, scale: 3, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItems", x => x.SaleItemId);
                    table.ForeignKey(
                        name: "FK_SaleItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleItems_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "SaleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierInvoiceItems",
                columns: table => new
                {
                    SupplierInvoiceItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierInvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitCost = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: false),
                    DiscountPct = table.Column<decimal>(type: "numeric(7,3)", precision: 7, scale: 3, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierInvoiceItems", x => x.SupplierInvoiceItemId);
                    table.ForeignKey(
                        name: "FK_SupplierInvoiceItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierInvoiceItems_SupplierInvoices_SupplierInvoiceId",
                        column: x => x.SupplierInvoiceId,
                        principalTable: "SupplierInvoices",
                        principalColumn: "SupplierInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    InvoiceItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: false),
                    DiscountPct = table.Column<decimal>(type: "numeric(7,3)", precision: 7, scale: 3, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.InvoiceItemId);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(16,4)", precision: 16, scale: 4, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reference = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    Status = table.Column<int>(type: "payment_status", nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesReturns",
                columns: table => new
                {
                    SalesReturnId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reason = table.Column<string>(type: "text", unicode: false, nullable: true),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReturns", x => x.SalesReturnId);
                    table.ForeignKey(
                        name: "FK_SalesReturns_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesReturnItems",
                columns: table => new
                {
                    SalesReturnItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    SalesReturnId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(14,4)", precision: 14, scale: 4, nullable: false),
                    State = table.Column<int>(type: "state_type", nullable: false),
                    AuditCreateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditCreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditUpdateUser = table.Column<int>(type: "integer", nullable: true),
                    AuditUpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AuditDeleteUser = table.Column<int>(type: "integer", nullable: true),
                    AuditDeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReturnItems", x => x.SalesReturnItemId);
                    table.ForeignKey(
                        name: "FK_SalesReturnItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReturnItems_SalesReturns_SalesReturnId",
                        column: x => x.SalesReturnId,
                        principalTable: "SalesReturns",
                        principalColumn: "SalesReturnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId_ProductId",
                table: "CartItems",
                columns: new[] { "CartId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId",
                table: "Carts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_CustomerId",
                table: "CustomerAddresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ID",
                table: "Customers",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ID",
                table: "Employees",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovements_ProductId",
                table: "InventoryMovements",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovements_ReferenceTable_ReferenceId",
                table: "InventoryMovements",
                columns: new[] { "ReferenceTable", "ReferenceId" });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovements_WarehouseId_ProductId",
                table: "InventoryMovements",
                columns: new[] { "WarehouseId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId_ProductId",
                table: "InvoiceItems",
                columns: new[] { "InvoiceId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ProductId",
                table: "InvoiceItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BillingAddressId",
                table: "Invoices",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceNumber",
                table: "Invoices",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrderId",
                table: "Invoices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SaleId",
                table: "Invoices",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId_ProductId",
                table: "OrderItems",
                columns: new[] { "OrderId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WarehouseId",
                table: "Orders",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WarehousesUUID",
                table: "Orders",
                column: "WarehousesUUID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_Code",
                table: "PaymentMethods",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Reference",
                table: "Payments",
                column: "Reference");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPriceHistory_ProductId_ValidFrom",
                table: "ProductPriceHistory",
                columns: new[] { "ProductId", "ValidFrom" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SKU",
                table: "Products",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TaxRateId",
                table: "Products",
                column: "TaxRateId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStock_ProductId",
                table: "ProductStock",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStock_WarehouseId_ProductId",
                table: "ProductStock",
                columns: new[] { "WarehouseId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_ProductId",
                table: "PurchaseOrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_PurchaseOrderId_ProductId",
                table: "PurchaseOrderItems",
                columns: new[] { "PurchaseOrderId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_OrderDate",
                table: "PurchaseOrders",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_WarehouseId",
                table: "PurchaseOrders",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_WarehousesUUID",
                table: "PurchaseOrders",
                column: "WarehousesUUID");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_ProductId",
                table: "SaleItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_SaleId_ProductId",
                table: "SaleItems",
                columns: new[] { "SaleId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SaleDate",
                table: "Sales",
                column: "SaleDate");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_WarehouseId",
                table: "Sales",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnItems_ProductId",
                table: "SalesReturnItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnItems_SalesReturnId_ProductId",
                table: "SalesReturnItems",
                columns: new[] { "SalesReturnId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturns_InvoiceId",
                table: "SalesReturns",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturns_ReturnDate",
                table: "SalesReturns",
                column: "ReturnDate");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierInvoiceItems_ProductId",
                table: "SupplierInvoiceItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierInvoiceItems_SupplierInvoiceId_ProductId",
                table: "SupplierInvoiceItems",
                columns: new[] { "SupplierInvoiceId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierInvoices_PurchaseOrderId",
                table: "SupplierInvoices",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierInvoices_SupplierId",
                table: "SupplierInvoices",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierInvoices_SupplierId_InvoiceNumber",
                table: "SupplierInvoices",
                columns: new[] { "SupplierId", "InvoiceNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_Email",
                table: "Suppliers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_ID",
                table: "Suppliers",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_Name",
                table: "Suppliers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxRates_Name",
                table: "TaxRates",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_Code",
                table: "Units",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_Name",
                table: "Units",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId_RoleId",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeId",
                table: "Users",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_Code",
                table: "Warehouses",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "InventoryMovements");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ProductPriceHistory");

            migrationBuilder.DropTable(
                name: "ProductStock");

            migrationBuilder.DropTable(
                name: "PurchaseOrderItems");

            migrationBuilder.DropTable(
                name: "SaleItems");

            migrationBuilder.DropTable(
                name: "SalesReturnItems");

            migrationBuilder.DropTable(
                name: "SupplierInvoiceItems");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "SalesReturns");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SupplierInvoices");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "TaxRates");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "CustomerAddresses");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Warehouses");
        }
    }
}
