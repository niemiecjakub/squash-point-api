using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SquashPointAPI.Migrations
{
    /// <inheritdoc />
    public partial class followers3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerFollow_AspNetUsers_FollowerId",
                table: "PlayerFollow");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6764861d-73fe-4af4-800f-a0a60623fa4c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e97a3274-1c67-4607-9663-0147340c609f");

            migrationBuilder.RenameColumn(
                name: "FollowerId",
                table: "PlayerFollow",
                newName: "FolloweeId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerFollow_FollowerId",
                table: "PlayerFollow",
                newName: "IX_PlayerFollow_FolloweeId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4723311a-edc6-421b-b31d-4783c58890cc", null, "Admin", "ADMIN" },
                    { "9690009d-1769-43f6-b58f-3b9eff94c8fa", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerFollow_AspNetUsers_FolloweeId",
                table: "PlayerFollow",
                column: "FolloweeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerFollow_AspNetUsers_FolloweeId",
                table: "PlayerFollow");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4723311a-edc6-421b-b31d-4783c58890cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9690009d-1769-43f6-b58f-3b9eff94c8fa");

            migrationBuilder.RenameColumn(
                name: "FolloweeId",
                table: "PlayerFollow",
                newName: "FollowerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerFollow_FolloweeId",
                table: "PlayerFollow",
                newName: "IX_PlayerFollow_FollowerId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6764861d-73fe-4af4-800f-a0a60623fa4c", null, "User", "USER" },
                    { "e97a3274-1c67-4607-9663-0147340c609f", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerFollow_AspNetUsers_FollowerId",
                table: "PlayerFollow",
                column: "FollowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
