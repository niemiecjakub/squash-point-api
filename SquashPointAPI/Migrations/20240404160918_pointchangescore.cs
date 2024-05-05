using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SquashPointAPI.Migrations
{
    /// <inheritdoc />
    public partial class pointchangescore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Point_AspNetUsers_WinnerId",
                table: "Point");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce4e09cf-34ce-43b1-bd52-81660f286869");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6da24b0-097c-4367-82a2-4c62e3b33837");

            migrationBuilder.DropColumn(
                name: "PointType",
                table: "Point");

            migrationBuilder.RenameColumn(
                name: "WinnerId",
                table: "Point",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Point_WinnerId",
                table: "Point",
                newName: "IX_Point_PlayerId");

            migrationBuilder.AddColumn<int>(
                name: "PointScore",
                table: "Point",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3ce232a2-338b-425c-b709-c249cc34af69", null, "User", "USER" },
                    { "d7b054d4-12df-41b8-88f5-1f1954663428", null, "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Point_AspNetUsers_PlayerId",
                table: "Point",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Point_AspNetUsers_PlayerId",
                table: "Point");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ce232a2-338b-425c-b709-c249cc34af69");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7b054d4-12df-41b8-88f5-1f1954663428");

            migrationBuilder.DropColumn(
                name: "PointScore",
                table: "Point");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Point",
                newName: "WinnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Point_PlayerId",
                table: "Point",
                newName: "IX_Point_WinnerId");

            migrationBuilder.AddColumn<string>(
                name: "PointType",
                table: "Point",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ce4e09cf-34ce-43b1-bd52-81660f286869", null, "Admin", "ADMIN" },
                    { "d6da24b0-097c-4367-82a2-4c62e3b33837", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Point_AspNetUsers_WinnerId",
                table: "Point",
                column: "WinnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
