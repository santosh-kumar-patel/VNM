using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VNMAPI.Migrations
{
    /// <inheritdoc />
    public partial class VNMAPI_adddcolumnTable_06_10_2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "40653f6f-ec3a-4cb7-87b1-14d623d6214a");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "b2123520-b651-4194-8fc2-33bff34aa8f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40653f6f-ec3a-4cb7-87b1-14d623d6214a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2123520-b651-4194-8fc2-33bff34aa8f0");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                schema: "dbo",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                schema: "dbo",
                table: "Employee",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9b2d71f8-b45a-4c01-8ebd-2c45aebb9c40", "1", "Admin", "Admin" },
                    { "f6b49665-da7e-4f76-846e-c4162020bada", "2", "User", "User" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserRole",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Status", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { "9b2d71f8-b45a-4c01-8ebd-2c45aebb9c40", "1", new DateTime(2024, 10, 7, 20, 27, 43, 868, DateTimeKind.Local).AddTicks(1863), true, "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "f6b49665-da7e-4f76-846e-c4162020bada", "1", new DateTime(2024, 10, 7, 20, 27, 43, 868, DateTimeKind.Local).AddTicks(1927), true, "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "9b2d71f8-b45a-4c01-8ebd-2c45aebb9c40");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "f6b49665-da7e-4f76-846e-c4162020bada");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b2d71f8-b45a-4c01-8ebd-2c45aebb9c40");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6b49665-da7e-4f76-846e-c4162020bada");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                schema: "dbo",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                schema: "dbo",
                table: "Employee");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "40653f6f-ec3a-4cb7-87b1-14d623d6214a", "1", "Admin", "Admin" },
                    { "b2123520-b651-4194-8fc2-33bff34aa8f0", "2", "User", "User" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserRole",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Status", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { "40653f6f-ec3a-4cb7-87b1-14d623d6214a", "1", new DateTime(2024, 10, 6, 15, 52, 45, 860, DateTimeKind.Local).AddTicks(5087), true, "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "b2123520-b651-4194-8fc2-33bff34aa8f0", "1", new DateTime(2024, 10, 6, 15, 52, 45, 860, DateTimeKind.Local).AddTicks(5142), true, "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }
    }
}
