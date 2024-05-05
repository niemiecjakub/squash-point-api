using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SquashPointAPI.Migrations
{
    /// <inheritdoc />
    public partial class leagueFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e41b8b9-aadd-4019-845c-cfe97ad494a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f3b047a-1ecf-4a46-9e24-adeccdfc0b78");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FollowerFollowee");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Leagues",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaxPlayers",
                table: "Leagues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Public",
                table: "Leagues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7e0619de-87b9-41ac-8dfb-aa46c4a2f6da", null, "User", "USER" },
                    { "906f340b-40bd-4dc5-9736-15f4062567bf", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e0619de-87b9-41ac-8dfb-aa46c4a2f6da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "906f340b-40bd-4dc5-9736-15f4062567bf");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "MaxPlayers",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "Public",
                table: "Leagues");

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
                    { "5e41b8b9-aadd-4019-845c-cfe97ad494a0", null, "Admin", "ADMIN" },
                    { "7f3b047a-1ecf-4a46-9e24-adeccdfc0b78", null, "User", "USER" }
                });
        }
    }
}
