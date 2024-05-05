using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SquashPointAPI.Migrations
{
    /// <inheritdoc />
    public partial class followers5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c8662bf-41d8-4396-b82f-c197bd6f3e35");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "baebf11b-31b5-4841-9b12-2bce90e51c39");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FollowerFollowee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5bd3e01b-b5c7-40fa-b92a-e85d81c220ea", null, "Admin", "ADMIN" },
                    { "70ca2554-29ac-4ea0-b6e8-5e0767ea7437", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bd3e01b-b5c7-40fa-b92a-e85d81c220ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70ca2554-29ac-4ea0-b6e8-5e0767ea7437");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FollowerFollowee");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7c8662bf-41d8-4396-b82f-c197bd6f3e35", null, "Admin", "ADMIN" },
                    { "baebf11b-31b5-4841-9b12-2bce90e51c39", null, "User", "USER" }
                });
        }
    }
}
