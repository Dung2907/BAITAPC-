using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Exercise02.Migrations
{
    /// <inheritdoc />
    public partial class CreateExercise02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Placeholder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_categories_categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpperName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iso3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumCode = table.Column<short>(type: "smallint", nullable: true),
                    PhoneCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimesUsed = table.Column<int>(type: "int", nullable: false),
                    MaxUsage = table.Column<int>(type: "int", nullable: true),
                    OrderAmountLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CouponStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CouponEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ComparePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BuyingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(165)", maxLength: 165, nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Published = table.Column<bool>(type: "bit", nullable: false),
                    DisableOutOfStock = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Privileges = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DialCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "galleries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Placeholder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsThumbnail = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_galleries_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_galleries_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_galleries_products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "product_categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_categories_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_categories_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_categories_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_shipping_info",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeightUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VolumeUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DimensionWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionDepth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_shipping_info", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_shipping_info_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCoupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CouponId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCoupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCoupons_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCoupons_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sells_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "staffaccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Placeholder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staffaccounts_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_staffaccounts_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CardItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardItems_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardItems_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VariantOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ComparePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BuyingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariantOptions_galleries_ImageId",
                        column: x => x.ImageId,
                        principalTable: "galleries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VariantOptions_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attributes_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_attributes_staffaccounts_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "staffaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_attributes_staffaccounts_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "staffaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seen = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotificationExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_staffaccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "staffaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_statuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Privacy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_statuses_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_statuses_staffaccounts_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "staffaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_statuses_staffaccounts_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "staffaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "shipping_zones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    FreeShipping = table.Column<bool>(type: "bit", nullable: false),
                    RateType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping_zones_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shipping_zones_staffaccounts_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "staffaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_shipping_zones_staffaccounts_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "staffaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "slideshows",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Placeholder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BtnLabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Published = table.Column<bool>(type: "bit", nullable: false),
                    Clicks = table.Column<int>(type: "int", nullable: false),
                    Styles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_slideshows_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_slideshows_staffaccounts_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "staffaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_slideshows_staffaccounts_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "staffaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_suppliers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_suppliers_staffaccounts_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "staffaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_suppliers_staffaccounts_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "staffaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tags_staffaccounts_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "staffaccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tags_staffaccounts_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "staffaccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "variants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariantOption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariantOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_variants_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_variants_VariantOptions_VariantOptionId",
                        column: x => x.VariantOptionId,
                        principalTable: "VariantOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_variants_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttributeValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeValueText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeValues_attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributes_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CouponId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderDeliveredCarrierDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderDeliveredCustomerDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedByStaffAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_order_statuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "order_statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_staffaccounts_UpdatedByStaffAccountId",
                        column: x => x.UpdatedByStaffAccountId,
                        principalTable: "staffaccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingZoneId = table.Column<int>(type: "int", nullable: false),
                    WeightUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NoMax = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingRates_shipping_zones_ShippingZoneId",
                        column: x => x.ShippingZoneId,
                        principalTable: "shipping_zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_tags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_tags_id", x => new { x.TagId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_product_tags_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_product_tags_tags_TagId",
                        column: x => x.TagId,
                        principalTable: "tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "product_attribute_values",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductAttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeValueId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_attribute_values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_attribute_values_AttributeValues_AttributeValueId",
                        column: x => x.AttributeValueId,
                        principalTable: "AttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_attribute_values_AttributeValues_AttributeValueId1",
                        column: x => x.AttributeValueId1,
                        principalTable: "AttributeValues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_attribute_values_ProductAttributes_ProductAttributeId",
                        column: x => x.ProductAttributeId,
                        principalTable: "ProductAttributes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VariantValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VariantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductAttributeValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariantValues_product_attribute_values_ProductAttributeValueId",
                        column: x => x.ProductAttributeValueId,
                        principalTable: "product_attribute_values",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariantValues_variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "BuyingPrice", "ComparePrice", "CreatedAt", "CreatedBy", "DisableOutOfStock", "Note", "ProductDescription", "ProductName", "ProductType", "Published", "Quantity", "SalePrice", "ShortDescription", "Sku", "Slug", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("395bd4d2-c1de-4678-aafc-46190f7deb94"), 999.99m, 1199.99m, new DateTime(2024, 8, 9, 7, 55, 41, 964, DateTimeKind.Utc).AddTicks(6147), new Guid("f8134ab5-7c16-466b-b83b-9b3a848a1b01"), true, "Không có ghi chú bổ sung.", "iPhone 15 Pro Max cung cấp công nghệ và tính năng tiên tiến.", "iPhone 15 Pro Max", "Smartphone", true, 5, 1099.99m, "Điện thoại thông minh thế hệ mới nhất", "444666", "iphone-15-pro-max", new Guid("204bebe4-8d54-44b7-b583-ff9d325f7aaf") },
                    { new Guid("bcef9364-5151-4275-9289-ea2bc6013472"), 899.99m, 1099.99m, new DateTime(2024, 8, 9, 7, 55, 41, 964, DateTimeKind.Utc).AddTicks(6117), new Guid("bb0e0aac-61a5-4d1e-a6b5-65c21180ba57"), true, "Không có ghi chú bổ sung.", "iPhone 14 Pro Max có màn hình lớn hơn và hệ thống camera được cải tiến.", "iPhone 14 Pro Max", "Smartphone", true, 10, 999.99m, "Điện thoại thông minh cao cấp", "880088", "iphone-14-pro-max", new Guid("935d102e-898e-4f6b-9c6b-02b46ab6156d") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_attributes_CreatedBy",
                table: "attributes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_attributes_UpdatedBy",
                table: "attributes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_AttributeId",
                table: "AttributeValues",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_CardItems_CardId",
                table: "CardItems",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardItems_ProductId",
                table: "CardItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CustomerId",
                table: "Cards",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_categories_ParentId",
                table: "categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddresses_CustomerId",
                table: "CustomerAddresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_galleries_ProductId",
                table: "galleries",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_galleries_ProductId1",
                table: "galleries",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AccountId",
                table: "Notifications",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_order_statuses_CreatedBy",
                table: "order_statuses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_order_statuses_UpdatedBy",
                table: "order_statuses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CouponId",
                table: "Orders",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UpdatedByStaffAccountId",
                table: "Orders",
                column: "UpdatedByStaffAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_product_attribute_values_AttributeValueId",
                table: "product_attribute_values",
                column: "AttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_product_attribute_values_AttributeValueId1",
                table: "product_attribute_values",
                column: "AttributeValueId1");

            migrationBuilder.CreateIndex(
                name: "IX_product_attribute_values_ProductAttributeId",
                table: "product_attribute_values",
                column: "ProductAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_product_categories_CategoryId",
                table: "product_categories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_product_categories_ProductId",
                table: "product_categories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_product_shipping_info_ProductId",
                table: "product_shipping_info",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_product_tags_ProductId",
                table: "product_tags",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AttributeId",
                table: "ProductAttributes",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductId",
                table: "ProductAttributes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCoupons_CouponId",
                table: "ProductCoupons",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCoupons_ProductId",
                table: "ProductCoupons",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_ProductId",
                table: "Sells",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_zones_CreatedBy",
                table: "shipping_zones",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_zones_UpdatedBy",
                table: "shipping_zones",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingRates_ShippingZoneId",
                table: "ShippingRates",
                column: "ShippingZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_slideshows_CreatedBy",
                table: "slideshows",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_slideshows_UpdatedBy",
                table: "slideshows",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_staffaccounts_RoleId",
                table: "staffaccounts",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_CountryId",
                table: "suppliers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_CreatedBy",
                table: "suppliers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_UpdatedBy",
                table: "suppliers",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tags_CreatedBy",
                table: "tags",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tags_UpdatedBy",
                table: "tags",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_VariantOptions_ImageId",
                table: "VariantOptions",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantOptions_ProductId",
                table: "VariantOptions",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_variants_ProductId",
                table: "variants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_variants_VariantOptionId",
                table: "variants",
                column: "VariantOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_ProductAttributeValueId",
                table: "VariantValues",
                column: "ProductAttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_VariantId",
                table: "VariantValues",
                column: "VariantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardItems");

            migrationBuilder.DropTable(
                name: "CustomerAddresses");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "product_categories");

            migrationBuilder.DropTable(
                name: "product_shipping_info");

            migrationBuilder.DropTable(
                name: "product_tags");

            migrationBuilder.DropTable(
                name: "ProductCoupons");

            migrationBuilder.DropTable(
                name: "Sells");

            migrationBuilder.DropTable(
                name: "ShippingRates");

            migrationBuilder.DropTable(
                name: "slideshows");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "VariantValues");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropTable(
                name: "shipping_zones");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "product_attribute_values");

            migrationBuilder.DropTable(
                name: "variants");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "order_statuses");

            migrationBuilder.DropTable(
                name: "AttributeValues");

            migrationBuilder.DropTable(
                name: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "VariantOptions");

            migrationBuilder.DropTable(
                name: "attributes");

            migrationBuilder.DropTable(
                name: "galleries");

            migrationBuilder.DropTable(
                name: "staffaccounts");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
