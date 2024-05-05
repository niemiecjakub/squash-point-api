using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SquashPointAPI.Migrations
{
    /// <inheritdoc />
    public partial class followers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cbfd80d-1ebf-4987-914f-1ca198def9f5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f090df67-1e94-4289-b12d-6b95be7d48ec");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8cbfd80d-1ebf-4987-914f-1ca198def9f5", null, "User", "USER" },
                    { "f090df67-1e94-4289-b12d-6b95be7d48ec", null, "Admin", "ADMIN" }
                });
        }
    }
}
