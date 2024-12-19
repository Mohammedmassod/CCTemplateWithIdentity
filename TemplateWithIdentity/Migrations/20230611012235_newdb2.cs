using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TemplateWithIdentity.Migrations
{
    /// <inheritdoc />
    public partial class newdb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b19e22d-a388-41ea-a2ae-14285f3af420");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48074259-6b83-4ac4-9e25-fc444629a9a9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f236893-611c-4df9-bab0-3ba8c43b6dd1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "Name_Ar", "NormalizedName" },
                values: new object[,]
                {
                    { "15f3d0fb-cce5-428f-9a88-f59a4acf1c8a", null, "Roles", "SuperAdmin", "مالك النظام", "SUPERADMIN" },
                    { "7d6e1e0d-0218-4bad-9adb-7dd674f091ff", null, "Roles", "Admin", "مدير النظام", "ADMIN" },
                    { "b1d04e3d-0002-4d5a-b7e6-075ab624c430", null, "Roles", "User", "مستخدم", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "IsClient", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "Pass", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4a2e1650-21bd-4e67-832e-2e99c267a2e4", 0, "18c386c9-3401-4a78-8cc1-5e8f3094036b", "Users", "Admin@gmail.com", false, true, false, null, "رئيس مجلس الادارة", "ADMIN@GMAIL.COM", "SUPERADMIN", "Admin123", "AQAAAAIAAYagAAAAEKgcJAHCcFO8MdHhmVgOXkZX+NNV5SBQLH171jWv7fapylJGayfwNI4x9Dzh9NJjlQ==", "778877887", false, "eb502f8a-ccb8-413d-845b-c2cbefcf8a60", false, "SuperAdmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15f3d0fb-cce5-428f-9a88-f59a4acf1c8a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d6e1e0d-0218-4bad-9adb-7dd674f091ff");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1d04e3d-0002-4d5a-b7e6-075ab624c430");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4a2e1650-21bd-4e67-832e-2e99c267a2e4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "Name_Ar", "NormalizedName" },
                values: new object[,]
                {
                    { "2b19e22d-a388-41ea-a2ae-14285f3af420", null, "Roles", "Lawyer", "محامي", "LAWYER" },
                    { "48074259-6b83-4ac4-9e25-fc444629a9a9", null, "Roles", "User", "مستخدم", "USER" },
                    { "7f236893-611c-4df9-bab0-3ba8c43b6dd1", null, "Roles", "Admin", "مدير النظام", "ADMIN" }
                });
        }
    }
}
