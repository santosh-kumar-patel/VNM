using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VNMAPI.Migrations
{
    /// <inheritdoc />
    public partial class VNMAPI_AddNewRoleTable_06_10_2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "85c24130-6e8b-474a-ae8b-7f554b89f1af", "1", "Admin", "Admin" },
                    { "b6ee674f-337a-4b3c-aca7-c156a502c636", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85c24130-6e8b-474a-ae8b-7f554b89f1af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6ee674f-337a-4b3c-aca7-c156a502c636");
        }
    }
}
