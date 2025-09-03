using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KHQ.Repo.Migrations
{
    /// <inheritdoc />
    public partial class updatesubproducts1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21cb8a6c-bf92-4cb3-a217-a1449b30c704");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "702340ca-8b4e-441d-ab9c-7e8b45bae5ea", null, "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "84d888e2-fcb8-4cf5-a27f-f167ed85d705", "AQAAAAIAAYagAAAAEFBQVDNdmNWE+Ze4MKKxN7ZRwYEKT2+8VmgA8gODgD/Cu8IN9XxLff6X2xtjfJHrDQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "702340ca-8b4e-441d-ab9c-7e8b45bae5ea");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

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
    }
}
