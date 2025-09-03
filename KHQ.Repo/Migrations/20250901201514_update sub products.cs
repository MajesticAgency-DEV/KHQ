using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KHQ.Repo.Migrations
{
    /// <inheritdoc />
    public partial class updatesubproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f06dfbc-6304-4daa-a2b1-95f9b280d227");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "SubProduct");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "21cb8a6c-bf92-4cb3-a217-a1449b30c704", null, "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d57ce1bd-362c-4cde-9e8c-e3a5ee034b20", "AQAAAAIAAYagAAAAEJyEsvPcmBtZ7jac9+/kZCF5P9Nmhm9Z4YOyHU6VO36qCpRfgijm1fZcJCO4rzeUYg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21cb8a6c-bf92-4cb3-a217-a1449b30c704");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "SubProduct",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9f06dfbc-6304-4daa-a2b1-95f9b280d227", null, "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e84ecab0-75a6-4ede-9e51-a427fbde0ec2", "AQAAAAIAAYagAAAAEJGtn2AKoaDYJlgung243+KPn9j+CjpWvS4c6fonrZk/Z/WXk9HwzAPL0WtpjBD92g==" });
        }
    }
}
