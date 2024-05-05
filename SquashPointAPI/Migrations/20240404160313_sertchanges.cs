using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SquashPointAPI.Migrations
{
    /// <inheritdoc />
    public partial class sertchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f6e5a63-5b03-440e-8754-0bedd65df947");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e24d10f3-357f-4ee7-bf73-9d77400fc1cb");

            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WinnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PointType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SetId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Point_AspNetUsers_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Point_Set_SetId",
                        column: x => x.SetId,
                        principalTable: "Set",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ce4e09cf-34ce-43b1-bd52-81660f286869", null, "Admin", "ADMIN" },
                    { "d6da24b0-097c-4367-82a2-4c62e3b33837", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Point_SetId",
                table: "Point",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Point_WinnerId",
                table: "Point",
                column: "WinnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Point");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce4e09cf-34ce-43b1-bd52-81660f286869");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6da24b0-097c-4367-82a2-4c62e3b33837");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f6e5a63-5b03-440e-8754-0bedd65df947", null, "User", "USER" },
                    { "e24d10f3-357f-4ee7-bf73-9d77400fc1cb", null, "Admin", "ADMIN" }
                });
        }
    }
}
