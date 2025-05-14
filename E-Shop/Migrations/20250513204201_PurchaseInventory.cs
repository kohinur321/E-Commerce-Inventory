using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Shop.Migrations
{
    public partial class PurchaseInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InventoryTypes",
                columns: table => new
                {
                    InventoryTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTypes", x => x.InventoryTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    GrandTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsApprove = table.Column<bool>(type: "bit", nullable: false),
                    SuppliersSupplierId = table.Column<int>(type: "int", nullable: true),
                    InventoryTypeId = table.Column<int>(type: "int", nullable: true),
                    StockTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_Purchases_Supplier_SuppliersSupplierId",
                        column: x => x.SuppliersSupplierId,
                        principalSchema: "Admin",
                        principalTable: "Supplier",
                        principalColumn: "SupplierId");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDetails",
                columns: table => new
                {
                    PurchaseDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    PurchasesPurchaseId = table.Column<int>(type: "int", nullable: true),
                    ProductsProductId = table.Column<int>(type: "int", nullable: true),
                    StoresStoreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetails", x => x.PurchaseDetailId);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_Product_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalSchema: "Admin",
                        principalTable: "Product",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_Purchases_PurchasesPurchaseId",
                        column: x => x.PurchasesPurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId");
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_Stores_StoresStoreId",
                        column: x => x.StoresStoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_ProductsProductId",
                table: "PurchaseDetails",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_PurchasesPurchaseId",
                table: "PurchaseDetails",
                column: "PurchasesPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_StoresStoreId",
                table: "PurchaseDetails",
                column: "StoresStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_SuppliersSupplierId",
                table: "Purchases",
                column: "SuppliersSupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryTypes");

            migrationBuilder.DropTable(
                name: "PurchaseDetails");

            migrationBuilder.DropTable(
                name: "Purchases");
        }
    }
}
