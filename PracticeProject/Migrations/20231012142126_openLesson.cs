using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeProject.Migrations
{
    /// <inheritdoc />
    public partial class openLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOpen",
                table: "Lessons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOpen",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Lessons");
        }
    }
}
