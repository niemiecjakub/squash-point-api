using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SquashPointAPI.Migrations
{
    /// <inheritdoc />
    public partial class imageFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48384dc4-76f1-4dbe-a71c-31363e0fe500");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e11ea54-bf30-40c2-bb8e-681d6617507a");

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a307689c-b61b-46a8-a735-39c525ac3fc0", null, "User", "USER" },
                    { "c8793307-4ce0-4454-bd5a-759b2b54e843", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a307689c-b61b-46a8-a735-39c525ac3fc0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8793307-4ce0-4454-bd5a-759b2b54e843");

            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "Images");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "48384dc4-76f1-4dbe-a71c-31363e0fe500", null, "Admin", "ADMIN" },
                    { "7e11ea54-bf30-40c2-bb8e-681d6617507a", null, "User", "USER" }
                });
        }
    }
}
