using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SquashPointAPI.Migrations
{
    /// <inheritdoc />
    public partial class followers4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerFollow");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4723311a-edc6-421b-b31d-4783c58890cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9690009d-1769-43f6-b58f-3b9eff94c8fa");

            migrationBuilder.CreateTable(
                name: "FollowerFollowee",
                columns: table => new
                {
                    FollowerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FolloweeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowerFollowee", x => new { x.FollowerId, x.FolloweeId });
                    table.ForeignKey(
                        name: "FK_FollowerFollowee_AspNetUsers_FolloweeId",
                        column: x => x.FolloweeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FollowerFollowee_AspNetUsers_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7c8662bf-41d8-4396-b82f-c197bd6f3e35", null, "Admin", "ADMIN" },
                    { "baebf11b-31b5-4841-9b12-2bce90e51c39", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowerFollowee_FolloweeId",
                table: "FollowerFollowee",
                column: "FolloweeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowerFollowee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c8662bf-41d8-4396-b82f-c197bd6f3e35");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "baebf11b-31b5-4841-9b12-2bce90e51c39");

            migrationBuilder.CreateTable(
                name: "PlayerFollow",
                columns: table => new
                {
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FolloweeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerFollow", x => new { x.PlayerId, x.FolloweeId });
                    table.ForeignKey(
                        name: "FK_PlayerFollow_AspNetUsers_FolloweeId",
                        column: x => x.FolloweeId,
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
                    { "4723311a-edc6-421b-b31d-4783c58890cc", null, "Admin", "ADMIN" },
                    { "9690009d-1769-43f6-b58f-3b9eff94c8fa", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerFollow_FolloweeId",
                table: "PlayerFollow",
                column: "FolloweeId");
        }
    }
}
