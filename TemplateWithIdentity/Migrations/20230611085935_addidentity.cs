using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TemplateWithIdentity.Migrations
{
    /// <inheritdoc />
    public partial class addidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "Name_Ar", "NormalizedName" },
                values: new object[,]
                {
                    { "205dd7b9-2084-4556-bef7-b684a1c34c18", null, "Roles", "User", "مستخدم", "USER" },
                    { "b78e0f03-99bd-4324-aaab-78aa6a102716", null, "Roles", "SuperAdmin", "مالك النظام", "SUPERADMIN" },
                    { "f867b15b-0cd2-49ed-84db-5ab874624f05", null, "Roles", "Admin", "مدير النظام", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4a2e1650-21bd-4e67-832e-2e99c267a2e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11fab487-f591-47a0-aa9d-fa614a0c9680", "AQAAAAIAAYagAAAAEOxUptUykqzBiOiN+v1bT03UD1yQCQLJfQ2ZaCyNHoY79bicBvqCRJcscSkBytVHcw==", "749d0fcd-875d-4393-a790-4233ab0d03f0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "205dd7b9-2084-4556-bef7-b684a1c34c18");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b78e0f03-99bd-4324-aaab-78aa6a102716");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f867b15b-0cd2-49ed-84db-5ab874624f05");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "Name_Ar", "NormalizedName" },
                values: new object[,]
                {
                    { "15f3d0fb-cce5-428f-9a88-f59a4acf1c8a", null, "Roles", "SuperAdmin", "مالك النظام", "SUPERADMIN" },
                    { "7d6e1e0d-0218-4bad-9adb-7dd674f091ff", null, "Roles", "Admin", "مدير النظام", "ADMIN" },
                    { "b1d04e3d-0002-4d5a-b7e6-075ab624c430", null, "Roles", "User", "مستخدم", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4a2e1650-21bd-4e67-832e-2e99c267a2e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18c386c9-3401-4a78-8cc1-5e8f3094036b", "AQAAAAIAAYagAAAAEKgcJAHCcFO8MdHhmVgOXkZX+NNV5SBQLH171jWv7fapylJGayfwNI4x9Dzh9NJjlQ==", "eb502f8a-ccb8-413d-845b-c2cbefcf8a60" });
        }
    }
}
