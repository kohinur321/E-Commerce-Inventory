using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Shop.Migrations
{
    public partial class order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Customer_CustomerId",
                schema: "User",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_CustomerId",
                schema: "User",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "User",
                table: "Cart");

            migrationBuilder.AddColumn<bool>(
                name: "isOrder",
                schema: "User",
                table: "Cart",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isOrder",
                schema: "User",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                schema: "User",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CustomerId",
                schema: "User",
                table: "Cart",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Customer_CustomerId",
                schema: "User",
                table: "Cart",
                column: "CustomerId",
                principalSchema: "User",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
