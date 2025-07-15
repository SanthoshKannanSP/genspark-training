using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceApi.Migrations
{
    /// <inheritdoc />
    public partial class TeacherUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Teachers_TeacherId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_Email",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_TeacherId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Sessions");

            migrationBuilder.AddColumn<string>(
                name: "TeacherEmail",
                table: "Sessions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Teachers_Email",
                table: "Teachers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TeacherEmail",
                table: "Sessions",
                column: "TeacherEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Teachers_TeacherEmail",
                table: "Sessions",
                column: "TeacherEmail",
                principalTable: "Teachers",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Teachers_TeacherEmail",
                table: "Sessions");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Teachers_Email",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_TeacherEmail",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "TeacherEmail",
                table: "Sessions");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Sessions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_Email",
                table: "Teachers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TeacherId",
                table: "Sessions",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Teachers_TeacherId",
                table: "Sessions",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
