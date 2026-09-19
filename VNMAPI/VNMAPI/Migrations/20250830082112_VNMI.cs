using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VNMAPI.Migrations
{
    /// <inheritdoc />
    public partial class VNMI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "db0c3a97-7a19-40c6-975f-5734c1d1a04a", "2", "User", "User" },
                    { "e9c74a6a-27d0-40bc-a630-0aa8c0954af3", "1", "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserRole",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Status", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { "db0c3a97-7a19-40c6-975f-5734c1d1a04a", "1", new DateTime(2025, 8, 30, 13, 51, 12, 212, DateTimeKind.Local).AddTicks(2297), true, "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "e9c74a6a-27d0-40bc-a630-0aa8c0954af3", "1", new DateTime(2025, 8, 30, 13, 51, 12, 212, DateTimeKind.Local).AddTicks(2244), true, "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "db0c3a97-7a19-40c6-975f-5734c1d1a04a");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "e9c74a6a-27d0-40bc-a630-0aa8c0954af3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db0c3a97-7a19-40c6-975f-5734c1d1a04a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9c74a6a-27d0-40bc-a630-0aa8c0954af3");

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
    }
}
