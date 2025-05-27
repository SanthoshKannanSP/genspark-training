using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SocialMediaAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HashtagsDb",
                columns: table => new
                {
                    TagName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashtagsDb", x => x.TagName);
                });

            migrationBuilder.CreateTable(
                name: "UsersDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TweetsDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    PostedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetsDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetsDb_UsersDb_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFollowsDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    FollowingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollowsDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFollowsDb_UsersDb_FollowingId",
                        column: x => x.FollowingId,
                        principalTable: "UsersDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikesDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TweetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikesDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikesDb_TweetsDb_TweetId",
                        column: x => x.TweetId,
                        principalTable: "TweetsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikesDb_UsersDb_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TweetHashtagsDb",
                columns: table => new
                {
                    TweetId = table.Column<int>(type: "integer", nullable: false),
                    HashtagName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetHashtagsDb", x => new { x.TweetId, x.HashtagName });
                    table.ForeignKey(
                        name: "FK_TweetHashtagsDb_HashtagsDb_HashtagName",
                        column: x => x.HashtagName,
                        principalTable: "HashtagsDb",
                        principalColumn: "TagName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TweetHashtagsDb_TweetsDb_TweetId",
                        column: x => x.TweetId,
                        principalTable: "TweetsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikesDb_TweetId",
                table: "LikesDb",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_LikesDb_UserId",
                table: "LikesDb",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetHashtagsDb_HashtagName",
                table: "TweetHashtagsDb",
                column: "HashtagName");

            migrationBuilder.CreateIndex(
                name: "IX_TweetsDb_UserId",
                table: "TweetsDb",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowsDb_FollowingId",
                table: "UserFollowsDb",
                column: "FollowingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikesDb");

            migrationBuilder.DropTable(
                name: "TweetHashtagsDb");

            migrationBuilder.DropTable(
                name: "UserFollowsDb");

            migrationBuilder.DropTable(
                name: "HashtagsDb");

            migrationBuilder.DropTable(
                name: "TweetsDb");

            migrationBuilder.DropTable(
                name: "UsersDb");
        }
    }
}
