using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceManagement.Api.Migrations
{
    public partial class AddYearInAttendanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "853346e1-d4f4-472c-95f7-327e26814e73");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd021bef-c544-40bd-8d4a-389ce42aaa18");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e0a1738f-b770-49c5-9818-935f62cba7a8", "49ff8909-19dd-4a0f-9959-6dd590556e8b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0a1738f-b770-49c5-9818-935f62cba7a8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "49ff8909-19dd-4a0f-9959-6dd590556e8b");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2b906077-677a-42ec-960c-4aadf91d045c", "ec0b7c2d-a799-4d32-bea5-8add1ac6cace", "Admin", "ADMIN" },
                    { "a6e53450-6b2b-47eb-8b41-ccf535de4af3", "56f9e2de-5e37-403d-9518-865eaa3d5a84", "Employee", "EMPLOYEE" },
                    { "d6689457-18ce-4d11-823e-4ba4883dca0a", "097e10e5-f280-477e-817a-26b9f79250a0", "Hr", "HR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a2e45ff8-27b2-444d-8a65-81546fc9ca22", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dd05110a-a759-45b7-b095-a1574b4116af", "admin@test.com", false, "Default", "Admin", false, null, "ADMIN@TEST.COM", "ADMIN", "AQAAAAEAACcQAAAAEJCjeVw+ljqjfggYdu/h4lwm0qnPVNOAJbox7pgsYbj2fR+N79oNVy0+Di+IlANB0g==", null, false, "cc4a8516-c5bf-4649-9e2e-49c02ed4c64c", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2b906077-677a-42ec-960c-4aadf91d045c", "a2e45ff8-27b2-444d-8a65-81546fc9ca22" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6e53450-6b2b-47eb-8b41-ccf535de4af3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6689457-18ce-4d11-823e-4ba4883dca0a");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2b906077-677a-42ec-960c-4aadf91d045c", "a2e45ff8-27b2-444d-8a65-81546fc9ca22" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b906077-677a-42ec-960c-4aadf91d045c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2e45ff8-27b2-444d-8a65-81546fc9ca22");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Attendances");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "853346e1-d4f4-472c-95f7-327e26814e73", "3cc38019-9d79-4468-a8bb-7abbb7b761f1", "Hr", "HR" },
                    { "dd021bef-c544-40bd-8d4a-389ce42aaa18", "488fb64b-83a9-4bc5-94a0-7642bf5f6dea", "Employee", "EMPLOYEE" },
                    { "e0a1738f-b770-49c5-9818-935f62cba7a8", "5b776d39-aad9-404b-8ce3-786631999226", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "49ff8909-19dd-4a0f-9959-6dd590556e8b", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cf290bcf-43b7-4549-9dba-d8a6b7c92d8e", "admin@test.com", false, "Default", "Admin", false, null, "ADMIN@TEST.COM", "ADMIN", "AQAAAAEAACcQAAAAEAF0hJMRZX7SPukuRLWJKb6903ZpzAL/6RJ9/3kKjTpJS8bp1Bku89c0PkEeUHMs7A==", null, false, "462debe8-ba45-4f29-9669-b07610e78421", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e0a1738f-b770-49c5-9818-935f62cba7a8", "49ff8909-19dd-4a0f-9959-6dd590556e8b" });
        }
    }
}
