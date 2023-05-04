using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceManagement.Infrastructure.Migrations
{
    public partial class AddedAuditTrailInAttendanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a724d7a4-0cf0-4f11-9745-03c44f6c8aee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e81f4c80-a9f4-4fe5-b685-653a25dde5d1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bc1ce909-c93d-4c2c-811b-f29feb39a7e7", "0f40e9e8-c47a-4e67-a83d-7eb3de0c3c6f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc1ce909-c93d-4c2c-811b-f29feb39a7e7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0f40e9e8-c47a-4e67-a83d-7eb3de0c3c6f");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1788e574-0054-444f-b36c-fe6c5b1e65ec", "adb90d23-ae9c-4864-8fe3-269197608691", "Employee", "EMPLOYEE" },
                    { "36081901-b9f6-4765-9d58-621b4d58c0a7", "1db68f82-e77e-44e9-b613-1e396a7224a5", "Admin", "ADMIN" },
                    { "dd7612eb-511d-4c9a-a275-4e094ac32086", "14053ca1-cf97-47cc-8122-b5fba4d2f052", "Hr", "HR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "50ce6331-68cb-4d9c-ac3c-c33891a76f67", 0, null, "850159c8-aa34-40d6-9c18-34089da77761", "admin@test.com", false, "Default", "Admin", false, null, "ADMIN@TEST.COM", "ADMIN", "AQAAAAEAACcQAAAAEI1sK8sDyFJ5yoSO5W4dvArNAd3u7qPDSbi5PsCAUGvuoU7Hwv+PLhhGSjlRU7I/aQ==", null, false, "a73c4226-a29b-469b-9988-c57e1940d515", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "36081901-b9f6-4765-9d58-621b4d58c0a7", "50ce6331-68cb-4d9c-ac3c-c33891a76f67" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1788e574-0054-444f-b36c-fe6c5b1e65ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd7612eb-511d-4c9a-a275-4e094ac32086");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "36081901-b9f6-4765-9d58-621b4d58c0a7", "50ce6331-68cb-4d9c-ac3c-c33891a76f67" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36081901-b9f6-4765-9d58-621b4d58c0a7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "50ce6331-68cb-4d9c-ac3c-c33891a76f67");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Attendances");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a724d7a4-0cf0-4f11-9745-03c44f6c8aee", "78f4ff4a-5cb7-4112-8678-ecee975002ed", "Hr", "HR" },
                    { "bc1ce909-c93d-4c2c-811b-f29feb39a7e7", "52b183bc-94e3-4962-8875-8929f79ba6d4", "Admin", "ADMIN" },
                    { "e81f4c80-a9f4-4fe5-b685-653a25dde5d1", "6f2dcdfb-fe9c-46f4-a8c2-03be0e53f7e1", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0f40e9e8-c47a-4e67-a83d-7eb3de0c3c6f", 0, null, "fdb7ade3-d867-4706-9d66-bdbc88d3c069", "admin@test.com", false, "Default", "Admin", false, null, "ADMIN@TEST.COM", "ADMIN", "AQAAAAEAACcQAAAAEIjZwEV3OhiG+RCBbHy5DVGdxaMM0mYV/Hl7o49UXxqVkzmJ1NsXea6W3uvs79YwjA==", null, false, "d31b237e-4777-48e2-84cc-26b647f7ed4c", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "bc1ce909-c93d-4c2c-811b-f29feb39a7e7", "0f40e9e8-c47a-4e67-a83d-7eb3de0c3c6f" });
        }
    }
}
