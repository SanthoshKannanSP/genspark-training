using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SocialMediaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowsDb_UsersDb_FollowingId",
                table: "UserFollowsDb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFollowsDb",
                table: "UserFollowsDb");

            migrationBuilder.DropIndex(
                name: "IX_UserFollowsDb_FollowingId",
                table: "UserFollowsDb");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserFollowsDb",
                newName: "FollowerId");

            migrationBuilder.AlterColumn<int>(
                name: "FollowerId",
                table: "UserFollowsDb",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "FollowedId",
                table: "UserFollowsDb",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFollowsDb",
                table: "UserFollowsDb",
                columns: new[] { "FollowedId", "FollowerId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowsDb_FollowerId",
                table: "UserFollowsDb",
                column: "FollowerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowsDb_UsersDb_FollowedId",
                table: "UserFollowsDb",
                column: "FollowedId",
                principalTable: "UsersDb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowsDb_UsersDb_FollowerId",
                table: "UserFollowsDb",
                column: "FollowerId",
                principalTable: "UsersDb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowsDb_UsersDb_FollowedId",
                table: "UserFollowsDb");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowsDb_UsersDb_FollowerId",
                table: "UserFollowsDb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFollowsDb",
                table: "UserFollowsDb");

            migrationBuilder.DropIndex(
                name: "IX_UserFollowsDb_FollowerId",
                table: "UserFollowsDb");

            migrationBuilder.DropColumn(
                name: "FollowedId",
                table: "UserFollowsDb");

            migrationBuilder.RenameColumn(
                name: "FollowerId",
                table: "UserFollowsDb",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserFollowsDb",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFollowsDb",
                table: "UserFollowsDb",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowsDb_FollowingId",
                table: "UserFollowsDb",
                column: "FollowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowsDb_UsersDb_FollowingId",
                table: "UserFollowsDb",
                column: "FollowingId",
                principalTable: "UsersDb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
