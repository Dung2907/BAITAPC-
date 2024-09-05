using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranAnhDung.API.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateExercise03DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "dbo",
                columns: table => new
                {
                    CategoryId = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    Summary = table.Column<string>(type: "varchar(255)", nullable: true),
                    Photo = table.Column<string>(type: "varchar(max)", nullable: true),
                    IsParent = table.Column<short>(type: "SMALLINT", nullable: false),
                    ParentId = table.Column<long>(type: "BIGINT", nullable: true),
                    AddedBy = table.Column<long>(type: "BIGINT", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "dbo",
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Users_AddedBy",
                        column: x => x.AddedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "dbo",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "BIGINT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(255)", nullable: false),
                    Slug = table.Column<string>(type: "varchar(255)", nullable: false),
                    Summary = table.Column<string>(type: "varchar(500)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Photo = table.Column<string>(type: "varchar(max)", nullable: false),
                    Stock = table.Column<int>(type: "INT", nullable: false),
                    Size = table.Column<string>(type: "varchar(50)", nullable: true),
                    Condition = table.Column<string>(type: "varchar(50)", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    IsFeatured = table.Column<short>(type: "SMALLINT", nullable: false),
                    CatId = table.Column<long>(type: "BIGINT", nullable: true),
                    ChildCatId = table.Column<long>(type: "BIGINT", nullable: true),
                    BrandId = table.Column<long>(type: "BIGINT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category_CatId",
                        column: x => x.CatId,
                        principalSchema: "dbo",
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_AddedBy",
                schema: "dbo",
                table: "Category",
                column: "AddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                schema: "dbo",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CatId",
                schema: "dbo",
                table: "Product",
                column: "CatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
