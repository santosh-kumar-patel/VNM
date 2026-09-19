using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VNMAPI.Migrations
{
    /// <inheritdoc />
    public partial class VNMAPI_updateTable_06_10_2024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "dbo",
                table: "UserRole",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "dbo",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "dbo",
                table: "UserRole",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                schema: "dbo",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
