using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VNMAPI.Migrations
{
    /// <inheritdoc />
    public partial class VNMAPI_InsertRoleTable_06_10_2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85c24130-6e8b-474a-ae8b-7f554b89f1af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6ee674f-337a-4b3c-aca7-c156a502c636");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b98fcbc-c7b2-4171-b828-db245476a4e1", "2", "User", "User" },
                    { "fac21133-4bfe-49fa-8fef-f9817c493352", "1", "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserRole",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Status", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { "0b98fcbc-c7b2-4171-b828-db245476a4e1", "1", new DateTime(2024, 10, 6, 15, 44, 27, 406, DateTimeKind.Local).AddTicks(7423), true, "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "fac21133-4bfe-49fa-8fef-f9817c493352", "1", new DateTime(2024, 10, 6, 15, 44, 27, 406, DateTimeKind.Local).AddTicks(7375), true, "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "0b98fcbc-c7b2-4171-b828-db245476a4e1");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRole",
                keyColumn: "Id",
                keyValue: "fac21133-4bfe-49fa-8fef-f9817c493352");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b98fcbc-c7b2-4171-b828-db245476a4e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fac21133-4bfe-49fa-8fef-f9817c493352");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "85c24130-6e8b-474a-ae8b-7f554b89f1af", "1", "Admin", "Admin" },
                    { "b6ee674f-337a-4b3c-aca7-c156a502c636", "2", "User", "User" }
                });
        }
    }
}
