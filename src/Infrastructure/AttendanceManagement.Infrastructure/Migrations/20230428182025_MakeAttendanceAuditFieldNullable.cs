using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceManagement.Infrastructure.Migrations
{
    public partial class MakeAttendanceAuditFieldNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Attendances",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Attendances",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "30f3e5e4-3d82-4fb0-9e3b-f517ea8a76b0", "4b403810-3638-4504-b35b-afe72ac34c78", "Admin", "ADMIN" },
                    { "b5bd80be-e99b-4658-9100-51b4257b557d", "309549cc-2056-4e9c-a55a-56194d32ca3a", "Employee", "EMPLOYEE" },
                    { "bf6da5f9-fa28-430f-b68c-ccd1114467d2", "bb1085ed-b46b-4fe3-b1d8-ed1638441e90", "Hr", "HR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0d1652cb-86b4-4dd4-9c1a-07bd8d0c2d81", 0, null, "1482d178-6fdb-4d88-8ba1-1196ab95d2c3", "admin@test.com", false, "Default", "Admin", false, null, "ADMIN@TEST.COM", "ADMIN", "AQAAAAEAACcQAAAAEBRGLfqzNBdFOLCsgVP7TMXQg06s/lwZ96RoIym7u5jkbuNK/ePtbflwC6NuilWD7g==", null, false, "c203cab2-0b6a-4c76-8e53-189f7fff7e19", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "30f3e5e4-3d82-4fb0-9e3b-f517ea8a76b0", "0d1652cb-86b4-4dd4-9c1a-07bd8d0c2d81" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5bd80be-e99b-4658-9100-51b4257b557d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf6da5f9-fa28-430f-b68c-ccd1114467d2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "30f3e5e4-3d82-4fb0-9e3b-f517ea8a76b0", "0d1652cb-86b4-4dd4-9c1a-07bd8d0c2d81" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30f3e5e4-3d82-4fb0-9e3b-f517ea8a76b0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0d1652cb-86b4-4dd4-9c1a-07bd8d0c2d81");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                table: "Attendances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Attendances",
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
    }
}
