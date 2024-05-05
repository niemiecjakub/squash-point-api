using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SquashPointAPI.Migrations
{
    /// <inheritdoc />
    public partial class score : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70404e23-3265-49fd-81a5-fbe15d6ab9c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b89d1b09-7943-4baa-98bc-e0062a6a18ab");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f6e5a63-5b03-440e-8754-0bedd65df947", null, "User", "USER" },
                    { "e24d10f3-357f-4ee7-bf73-9d77400fc1cb", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f6e5a63-5b03-440e-8754-0bedd65df947");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e24d10f3-357f-4ee7-bf73-9d77400fc1cb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "70404e23-3265-49fd-81a5-fbe15d6ab9c7", null, "User", "USER" },
                    { "b89d1b09-7943-4baa-98bc-e0062a6a18ab", null, "Admin", "ADMIN" }
                });
        }
    }
}
