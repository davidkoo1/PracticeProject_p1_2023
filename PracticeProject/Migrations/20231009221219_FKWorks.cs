using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeProject.Migrations
{
    /// <inheritdoc />
    public partial class FKWorks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabWorks_Lessons_LessonId",
                table: "LabWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_TextWorks_Lessons_LessonId",
                table: "TextWorks");

            migrationBuilder.DropIndex(
                name: "IX_TextWorks_LessonId",
                table: "TextWorks");

            migrationBuilder.DropIndex(
                name: "IX_LabWorks_LessonId",
                table: "LabWorks");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "TextWorks");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "LabWorks");

            migrationBuilder.CreateIndex(
                name: "IX_TextWorks_IdLesson",
                table: "TextWorks",
                column: "IdLesson");

            migrationBuilder.CreateIndex(
                name: "IX_LabWorks_IdLesson",
                table: "LabWorks",
                column: "IdLesson");

            migrationBuilder.AddForeignKey(
                name: "FK_LabWorks_Lessons_IdLesson",
                table: "LabWorks",
                column: "IdLesson",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TextWorks_Lessons_IdLesson",
                table: "TextWorks",
                column: "IdLesson",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabWorks_Lessons_IdLesson",
                table: "LabWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_TextWorks_Lessons_IdLesson",
                table: "TextWorks");

            migrationBuilder.DropIndex(
                name: "IX_TextWorks_IdLesson",
                table: "TextWorks");

            migrationBuilder.DropIndex(
                name: "IX_LabWorks_IdLesson",
                table: "LabWorks");

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "TextWorks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "LabWorks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TextWorks_LessonId",
                table: "TextWorks",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LabWorks_LessonId",
                table: "LabWorks",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabWorks_Lessons_LessonId",
                table: "LabWorks",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TextWorks_Lessons_LessonId",
                table: "TextWorks",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
