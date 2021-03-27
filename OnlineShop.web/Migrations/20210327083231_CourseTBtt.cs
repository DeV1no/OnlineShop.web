using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.web.Migrations
{
    public partial class CourseTBtt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseStatuses_StateId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StateId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Courses");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StatusId",
                table: "Courses",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseStatuses_StatusId",
                table: "Courses",
                column: "StatusId",
                principalTable: "CourseStatuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseStatuses_StatusId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StatusId",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StateId",
                table: "Courses",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseStatuses_StateId",
                table: "Courses",
                column: "StateId",
                principalTable: "CourseStatuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
