using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.web.Migrations
{
    public partial class PermissionsTT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParrentId",
                table: "Permission");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParrentId",
                table: "Permission",
                type: "int",
                nullable: true);
        }
    }
}
