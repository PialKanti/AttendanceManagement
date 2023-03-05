using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceManagement.Api.Migrations
{
    public partial class AttendanceMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64ada571-fb10-4f24-b608-042e3f1b34dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4b7fc5e-42e3-469e-8175-a469b00452ab");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b1ed9659-aa09-4c63-b734-b8c355180301", "4603e2db-59f4-449a-9014-61c1bd403939" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1ed9659-aa09-4c63-b734-b8c355180301");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4603e2db-59f4-449a-9014-61c1bd403939");

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EntryTimestamp = table.Column<long>(type: "bigint", nullable: false),
                    ExitTimestamp = table.Column<long>(type: "bigint", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "60f0dbc7-ca24-4e78-a121-2c66b0dfd09f", "7508fbd4-0875-4a83-92a1-d7a09d98d562", "Hr", "HR" },
                    { "e0c8a9da-ddc1-45e5-8f1f-552cddfd7193", "c9b246ed-83d6-421d-aa23-6339e20bc7bb", "Employee", "EMPLOYEE" },
                    { "f8e27e73-f812-4fb1-9422-9d24674e46c9", "c730a198-a8a0-46b5-ad0d-393a796b2e31", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "59674b8c-80c5-452d-84dd-9e8f53a339dc", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "a45bb31c-aa5b-4597-bd8a-03c1441240f9", "admin@test.com", false, "Default", "Admin", false, null, "ADMIN@TEST.COM", "ADMIN", "AQAAAAEAACcQAAAAEFM1DTQ4IhHYqd9E5iy/4aoYYYMhi74bFSsql5oL41zRV5ivb6HiaF+P847tZQ0BTA==", null, false, "95a3c11d-2ac7-4fb4-8b29-2e8a912bb679", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f8e27e73-f812-4fb1-9422-9d24674e46c9", "59674b8c-80c5-452d-84dd-9e8f53a339dc" });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_UserId",
                table: "Attendances",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60f0dbc7-ca24-4e78-a121-2c66b0dfd09f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0c8a9da-ddc1-45e5-8f1f-552cddfd7193");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f8e27e73-f812-4fb1-9422-9d24674e46c9", "59674b8c-80c5-452d-84dd-9e8f53a339dc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8e27e73-f812-4fb1-9422-9d24674e46c9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "59674b8c-80c5-452d-84dd-9e8f53a339dc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "64ada571-fb10-4f24-b608-042e3f1b34dd", "ceed144d-e178-4071-a416-cd33f30f7044", "Hr", "HR" },
                    { "b1ed9659-aa09-4c63-b734-b8c355180301", "df738ed2-37e8-448a-8866-4b99d93a569c", "Admin", "ADMIN" },
                    { "c4b7fc5e-42e3-469e-8175-a469b00452ab", "6ee6fcaf-fcd8-403c-880f-e8a345906f4e", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4603e2db-59f4-449a-9014-61c1bd403939", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "10c3731b-f4a3-4aad-a83b-bd4fed97781d", "admin@test.com", false, "Default", "Admin", false, null, "ADMIN@TEST.COM", "ADMIN", "AQAAAAEAACcQAAAAEOkGOwHuZxQbORSyR+IHgeHpx1LOjn8uODXtQQM/yfDJ8doJ/aenZ919JtYx9dI7sw==", null, false, "ab025fae-3a42-41fd-be03-f72d78c93839", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b1ed9659-aa09-4c63-b734-b8c355180301", "4603e2db-59f4-449a-9014-61c1bd403939" });
        }
    }
}
