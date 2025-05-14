using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Shop.Migrations
{
    public partial class customar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                schema: "Admin",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                schema: "Admin",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CartId",
                schema: "Admin",
                table: "Order",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Cart_CartId",
                schema: "Admin",
                table: "Order",
                column: "CartId",
                principalSchema: "User",
                principalTable: "Cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Cart_CartId",
                schema: "Admin",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CartId",
                schema: "Admin",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CartId",
                schema: "Admin",
                table: "Order");

            migrationBuilder.AddColumn<double>(
                name: "TotalAmount",
                schema: "Admin",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
