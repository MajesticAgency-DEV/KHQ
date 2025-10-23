using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KHQ.Repo.Migrations
{
    /// <inheritdoc />
    public partial class changeadminemail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9621c564-d946-4c30-97c1-83af84b42cec");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "864a8a72-f7ce-4a8f-b00a-bc8a42e583ba", null, "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "a9bac53e-8c5f-4034-86e0-5e06dd3549f8", "info@kh-alqastal.com", "info@kh-alqastal.com", "AQAAAAIAAYagAAAAENZujE7XvlW94K+eHpS7h0Qrhr9RPxEcc5dIAXQ/Ey3qZcR9y0p+2gvHUdx8bYEi1w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "864a8a72-f7ce-4a8f-b00a-bc8a42e583ba");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9621c564-d946-4c30-97c1-83af84b42cec", null, "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "9fefe49c-116f-487f-9cdc-ecb697bbbeff", "Admin@KHQ.com", "Admin@KHQ.com", "AQAAAAIAAYagAAAAEAAnXhDBvFgQl1rxRnm0aSOHbC6Pis3F13hsNmuT5nIq6OJgEuKrJnuc/ypDfKtPdA==" });
        }
    }
}
