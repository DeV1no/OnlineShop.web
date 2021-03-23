using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.web.Migrations
{
    public partial class waletTableTT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_WalletTypes_WalletTypeTypeId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_WalletTypeTypeId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "WalletTypeTypeId",
                table: "Wallet");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_TypeId",
                table: "Wallet",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_WalletTypes_TypeId",
                table: "Wallet",
                column: "TypeId",
                principalTable: "WalletTypes",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_WalletTypes_TypeId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_TypeId",
                table: "Wallet");

            migrationBuilder.AddColumn<int>(
                name: "WalletTypeTypeId",
                table: "Wallet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_WalletTypeTypeId",
                table: "Wallet",
                column: "WalletTypeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_WalletTypes_WalletTypeTypeId",
                table: "Wallet",
                column: "WalletTypeTypeId",
                principalTable: "WalletTypes",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
