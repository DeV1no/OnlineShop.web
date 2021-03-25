using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.web.Migrations
{
    public partial class PermissionsToList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permission_Permission_ParentId",
                table: "Permission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermisson_Permission_PermissionId",
                table: "RolePermisson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permission",
                table: "Permission");

            migrationBuilder.RenameTable(
                name: "Permission",
                newName: "Permision");

            migrationBuilder.RenameIndex(
                name: "IX_Permission_ParentId",
                table: "Permision",
                newName: "IX_Permision_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permision",
                table: "Permision",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permision_Permision_ParentId",
                table: "Permision",
                column: "ParentId",
                principalTable: "Permision",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermisson_Permision_PermissionId",
                table: "RolePermisson",
                column: "PermissionId",
                principalTable: "Permision",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permision_Permision_ParentId",
                table: "Permision");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermisson_Permision_PermissionId",
                table: "RolePermisson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permision",
                table: "Permision");

            migrationBuilder.RenameTable(
                name: "Permision",
                newName: "Permission");

            migrationBuilder.RenameIndex(
                name: "IX_Permision_ParentId",
                table: "Permission",
                newName: "IX_Permission_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permission",
                table: "Permission",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permission_Permission_ParentId",
                table: "Permission",
                column: "ParentId",
                principalTable: "Permission",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermisson_Permission_PermissionId",
                table: "RolePermisson",
                column: "PermissionId",
                principalTable: "Permission",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
