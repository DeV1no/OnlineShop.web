using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.web.Migrations
{
    public partial class commentTBLTT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseDate",
                table: "CourseComments",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "CourseComments",
                newName: "CourseDate");
        }
    }
}
