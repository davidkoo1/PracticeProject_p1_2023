using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PracticeProject.Migrations
{
    /// <inheritdoc />
    public partial class ListsWorkLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabWorks_Lessons_IdLesson",
                table: "LabWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_TextWorks_Lessons_IdLesson",
                table: "TextWorks");

            migrationBuilder.DropColumn(
                name: "IdCourse",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "TextWorks",
                newName: "Task");

            migrationBuilder.RenameColumn(
                name: "IdLesson",
                table: "TextWorks",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_TextWorks_IdLesson",
                table: "TextWorks",
                newName: "IX_TextWorks_LessonId");

            migrationBuilder.RenameColumn(
                name: "IdLesson",
                table: "LabWorks",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_LabWorks_IdLesson",
                table: "LabWorks",
                newName: "IX_LabWorks_LessonId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TextWorks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "LabWorks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextWorks",
                table: "TextWorks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LabWorks",
                table: "LabWorks",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabWorks_Lessons_LessonId",
                table: "LabWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_TextWorks_Lessons_LessonId",
                table: "TextWorks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TextWorks",
                table: "TextWorks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LabWorks",
                table: "LabWorks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TextWorks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LabWorks");

            migrationBuilder.RenameColumn(
                name: "Task",
                table: "TextWorks",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "TextWorks",
                newName: "IdLesson");

            migrationBuilder.RenameIndex(
                name: "IX_TextWorks_LessonId",
                table: "TextWorks",
                newName: "IX_TextWorks_IdLesson");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "LabWorks",
                newName: "IdLesson");

            migrationBuilder.RenameIndex(
                name: "IX_LabWorks_LessonId",
                table: "LabWorks",
                newName: "IX_LabWorks_IdLesson");

            migrationBuilder.AddColumn<int>(
                name: "IdCourse",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
