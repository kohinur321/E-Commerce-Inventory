using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Shop.Migrations
{
    public partial class tranled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeModelTransactionTypeId",
                table: "Ledgers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_TransactionTypeModelTransactionTypeId",
                table: "Ledgers",
                column: "TransactionTypeModelTransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_Transactions_TransactionTypeModelTransactionTypeId",
                table: "Ledgers",
                column: "TransactionTypeModelTransactionTypeId",
                principalTable: "Transactions",
                principalColumn: "TransactionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_Transactions_TransactionTypeModelTransactionTypeId",
                table: "Ledgers");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Ledgers_TransactionTypeModelTransactionTypeId",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "TransactionTypeModelTransactionTypeId",
                table: "Ledgers");
        }
    }
}
