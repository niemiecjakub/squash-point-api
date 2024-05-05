using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SquashPointAPI.Migrations
{
    /// <inheritdoc />
    public partial class followers2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerPlayer");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f2c2a8f-89b3-4b17-ac5d-4847bc9a5e14");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a16ddafa-e5ad-441a-98a0-2b3ad2d576bf");

            migrationBuilder.CreateTable(
                name: "PlayerFollow",
                columns: table => new
                {
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerFollow", x => new { x.PlayerId, x.FollowerId });
                    table.ForeignKey(
                        name: "FK_PlayerFollow_AspNetUsers_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayerFollow_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6764861d-73fe-4af4-800f-a0a60623fa4c", null, "User", "USER" },
                    { "e97a3274-1c67-4607-9663-0147340c609f", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerFollow_FollowerId",
                table: "PlayerFollow",
                column: "FollowerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerFollow");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6764861d-73fe-4af4-800f-a0a60623fa4c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e97a3274-1c67-4607-9663-0147340c609f");

            migrationBuilder.CreateTable(
                name: "PlayerPlayer",
                columns: table => new
                {
                    FollowersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowingId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerPlayer", x => new { x.FollowersId, x.FollowingId });
                    table.ForeignKey(
                        name: "FK_PlayerPlayer_AspNetUsers_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerPlayer_AspNetUsers_FollowingId",
                        column: x => x.FollowingId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f2c2a8f-89b3-4b17-ac5d-4847bc9a5e14", null, "Admin", "ADMIN" },
                    { "a16ddafa-e5ad-441a-98a0-2b3ad2d576bf", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerPlayer_FollowingId",
                table: "PlayerPlayer",
                column: "FollowingId");
        }
    }
}
