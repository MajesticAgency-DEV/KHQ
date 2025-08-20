using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KHQ.Repo.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImageLinkCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5deb47b1-862d-4470-beea-1f65994d5d84");

            migrationBuilder.DropColumn(
                name: "ImageLink",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab369bd0-fd1a-4647-bb60-f1f6bea14f18", null, "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b07b8b74-e7d7-4ace-9864-a0092bc27888", "AQAAAAIAAYagAAAAEJVhHAkB88D3A+XzNmJ7eES1Llie1A1mlieD/R8l2BBsYFq3DPnu/nVuJyeTPnSkBA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab369bd0-fd1a-4647-bb60-f1f6bea14f18");

            migrationBuilder.AddColumn<string>(
                name: "ImageLink",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5deb47b1-862d-4470-beea-1f65994d5d84", null, "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5afed407-b0eb-4778-9c0d-88c2b2556363", "AQAAAAIAAYagAAAAENlPFnu/UGIukvkVMUsrcgqZYaIUM01R6dbRp9XyLa0MAe/H9VzT8w9OxgOrOqF5jw==" });
        }
    }
}
