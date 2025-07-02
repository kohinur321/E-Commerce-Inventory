using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Shop.Migrations
{
    public partial class Purchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_Stocks_StockModelStockId",
                table: "Ledgers");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Supplier_SuppliersSupplierId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_SuppliersSupplierId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "InventoryTypeId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "StockTypeId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "SuppliersSupplierId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Ledgers");

            migrationBuilder.RenameColumn(
                name: "TransactionTypeId",
                table: "Ledgers",
                newName: "StoreId");

            migrationBuilder.RenameColumn(
                name: "StockModelStockId",
                table: "Ledgers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "Ledgers",
                newName: "StockTypeId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Ledgers",
                newName: "Price");

            migrationBuilder.RenameIndex(
                name: "IX_Ledgers_StockModelStockId",
                table: "Ledgers",
                newName: "IX_Ledgers_UserId");

            migrationBuilder.AddColumn<int>(
                name: "InventoryTypeId",
                table: "Ledgers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Ledgers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Ledgers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_SupplierId",
                table: "Purchases",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_InventoryTypeId",
                table: "Ledgers",
                column: "InventoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_ProductId",
                table: "Ledgers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_StockTypeId",
                table: "Ledgers",
                column: "StockTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_StoreId",
                table: "Ledgers",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_InventoryTypes_InventoryTypeId",
                table: "Ledgers",
                column: "InventoryTypeId",
                principalTable: "InventoryTypes",
                principalColumn: "InventoryTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_Product_ProductId",
                table: "Ledgers",
                column: "ProductId",
                principalSchema: "Admin",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_StockTypes_StockTypeId",
                table: "Ledgers",
                column: "StockTypeId",
                principalTable: "StockTypes",
                principalColumn: "StockTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_Stores_StoreId",
                table: "Ledgers",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_Users_UserId",
                table: "Ledgers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Supplier_SupplierId",
                table: "Purchases",
                column: "SupplierId",
                principalSchema: "Admin",
                principalTable: "Supplier",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_InventoryTypes_InventoryTypeId",
                table: "Ledgers");

            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_Product_ProductId",
                table: "Ledgers");

            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_StockTypes_StockTypeId",
                table: "Ledgers");

            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_Stores_StoreId",
                table: "Ledgers");

            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_Users_UserId",
                table: "Ledgers");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Supplier_SupplierId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_SupplierId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Ledgers_InventoryTypeId",
                table: "Ledgers");

            migrationBuilder.DropIndex(
                name: "IX_Ledgers_ProductId",
                table: "Ledgers");

            migrationBuilder.DropIndex(
                name: "IX_Ledgers_StockTypeId",
                table: "Ledgers");

            migrationBuilder.DropIndex(
                name: "IX_Ledgers_StoreId",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "InventoryTypeId",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Ledgers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Ledgers",
                newName: "StockModelStockId");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Ledgers",
                newName: "TransactionTypeId");

            migrationBuilder.RenameColumn(
                name: "StockTypeId",
                table: "Ledgers",
                newName: "StockId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Ledgers",
                newName: "Amount");

            migrationBuilder.RenameIndex(
                name: "IX_Ledgers_UserId",
                table: "Ledgers",
                newName: "IX_Ledgers_StockModelStockId");

            migrationBuilder.AddColumn<int>(
                name: "InventoryTypeId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StockTypeId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SuppliersSupplierId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Ledgers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Ledgers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_SuppliersSupplierId",
                table: "Purchases",
                column: "SuppliersSupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_Stocks_StockModelStockId",
                table: "Ledgers",
                column: "StockModelStockId",
                principalTable: "Stocks",
                principalColumn: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Supplier_SuppliersSupplierId",
                table: "Purchases",
                column: "SuppliersSupplierId",
                principalSchema: "Admin",
                principalTable: "Supplier",
                principalColumn: "SupplierId");
        }
    }
}
