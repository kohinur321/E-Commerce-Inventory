using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Shop.Migrations
{
    public partial class upmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isOrder",
                schema: "User",
                table: "Cart");

            migrationBuilder.AddColumn<bool>(
                name: "isComplete",
                schema: "Admin",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isComplete",
                schema: "Admin",
                table: "Order");

            migrationBuilder.AddColumn<bool>(
                name: "isOrder",
                schema: "User",
                table: "Cart",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
