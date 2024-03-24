using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SquashPointAPI.Migrations
{
    /// <inheritdoc />
    public partial class ssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a307689c-b61b-46a8-a735-39c525ac3fc0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8793307-4ce0-4454-bd5a-759b2b54e843");

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Leagues",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9fdd0ba7-f225-4868-89d5-8c152866dc86", null, "Admin", "ADMIN" },
                    { "e38a61b8-9f89-4999-b902-49bdfbc904a3", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_PhotoId",
                table: "Leagues",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhotoId",
                table: "AspNetUsers",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_PhotoId",
                table: "AspNetUsers",
                column: "PhotoId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Images_PhotoId",
                table: "Leagues",
                column: "PhotoId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Images_PhotoId",
                table: "Leagues");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_PhotoId",
                table: "Leagues");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fdd0ba7-f225-4868-89d5-8c152866dc86");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e38a61b8-9f89-4999-b902-49bdfbc904a3");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a307689c-b61b-46a8-a735-39c525ac3fc0", null, "User", "USER" },
                    { "c8793307-4ce0-4454-bd5a-759b2b54e843", null, "Admin", "ADMIN" }
                });
        }
    }
}
