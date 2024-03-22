using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SquashPointAPI.Migrations
{
    /// <inheritdoc />
    public partial class league : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bd3e01b-b5c7-40fa-b92a-e85d81c220ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70ca2554-29ac-4ea0-b6e8-5e0767ea7437");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Leagues",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5e41b8b9-aadd-4019-845c-cfe97ad494a0", null, "Admin", "ADMIN" },
                    { "7f3b047a-1ecf-4a46-9e24-adeccdfc0b78", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_OwnerId",
                table: "Leagues",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_AspNetUsers_OwnerId",
                table: "Leagues",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_AspNetUsers_OwnerId",
                table: "Leagues");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_OwnerId",
                table: "Leagues");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e41b8b9-aadd-4019-845c-cfe97ad494a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f3b047a-1ecf-4a46-9e24-adeccdfc0b78");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Leagues");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5bd3e01b-b5c7-40fa-b92a-e85d81c220ea", null, "Admin", "ADMIN" },
                    { "70ca2554-29ac-4ea0-b6e8-5e0767ea7437", null, "User", "USER" }
                });
        }
    }
}
