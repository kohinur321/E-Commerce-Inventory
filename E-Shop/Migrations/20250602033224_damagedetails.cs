using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Shop.Migrations
{
    public partial class damagedetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageDetailModel_Damages_DamageId",
                table: "DamageDetailModel");

            migrationBuilder.DropForeignKey(
                name: "FK_DamageDetailModel_Product_ProductId",
                table: "DamageDetailModel");

            migrationBuilder.DropForeignKey(
                name: "FK_DamageDetailModel_Stores_StoreId",
                table: "DamageDetailModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DamageDetailModel",
                table: "DamageDetailModel");

            migrationBuilder.RenameTable(
                name: "DamageDetailModel",
                newName: "DamageDetails");

            migrationBuilder.RenameIndex(
                name: "IX_DamageDetailModel_StoreId",
                table: "DamageDetails",
                newName: "IX_DamageDetails_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_DamageDetailModel_ProductId",
                table: "DamageDetails",
                newName: "IX_DamageDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_DamageDetailModel_DamageId",
                table: "DamageDetails",
                newName: "IX_DamageDetails_DamageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DamageDetails",
                table: "DamageDetails",
                column: "DamageDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageDetails_Damages_DamageId",
                table: "DamageDetails",
                column: "DamageId",
                principalTable: "Damages",
                principalColumn: "DamageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DamageDetails_Product_ProductId",
                table: "DamageDetails",
                column: "ProductId",
                principalSchema: "Admin",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DamageDetails_Stores_StoreId",
                table: "DamageDetails",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageDetails_Damages_DamageId",
                table: "DamageDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_DamageDetails_Product_ProductId",
                table: "DamageDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_DamageDetails_Stores_StoreId",
                table: "DamageDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DamageDetails",
                table: "DamageDetails");

            migrationBuilder.RenameTable(
                name: "DamageDetails",
                newName: "DamageDetailModel");

            migrationBuilder.RenameIndex(
                name: "IX_DamageDetails_StoreId",
                table: "DamageDetailModel",
                newName: "IX_DamageDetailModel_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_DamageDetails_ProductId",
                table: "DamageDetailModel",
                newName: "IX_DamageDetailModel_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_DamageDetails_DamageId",
                table: "DamageDetailModel",
                newName: "IX_DamageDetailModel_DamageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DamageDetailModel",
                table: "DamageDetailModel",
                column: "DamageDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageDetailModel_Damages_DamageId",
                table: "DamageDetailModel",
                column: "DamageId",
                principalTable: "Damages",
                principalColumn: "DamageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DamageDetailModel_Product_ProductId",
                table: "DamageDetailModel",
                column: "ProductId",
                principalSchema: "Admin",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DamageDetailModel_Stores_StoreId",
                table: "DamageDetailModel",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
