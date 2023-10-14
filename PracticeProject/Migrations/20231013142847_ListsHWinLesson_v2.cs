using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeProject.Migrations
{
    /// <inheritdoc />
    public partial class ListsHWinLesson_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorks_Lessons_IdLesson",
                table: "HomeWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Courses_IdCourse",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_IdCourse",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_HomeWorks_IdLesson",
                table: "HomeWorks");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "HomeWorks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "HomeWorks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HomeWorks",
                table: "HomeWorks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeWorks_LessonId",
                table: "HomeWorks",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorks_Lessons_LessonId",
                table: "HomeWorks",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Courses_CourseId",
                table: "Lessons",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorks_Lessons_LessonId",
                table: "HomeWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Courses_CourseId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_CourseId",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HomeWorks",
                table: "HomeWorks");

            migrationBuilder.DropIndex(
                name: "IX_HomeWorks_LessonId",
                table: "HomeWorks");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "HomeWorks");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "HomeWorks");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_IdCourse",
                table: "Lessons",
                column: "IdCourse");

            migrationBuilder.CreateIndex(
                name: "IX_HomeWorks_IdLesson",
                table: "HomeWorks",
                column: "IdLesson");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorks_Lessons_IdLesson",
                table: "HomeWorks",
                column: "IdLesson",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Courses_IdCourse",
                table: "Lessons",
                column: "IdCourse",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
