using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KHQ.Repo.Migrations
{
    /// <inheritdoc />
    public partial class Addsortorderinsubproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e841caba-2e08-404e-a239-107f66a9cd28");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "SubProduct",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8cd0ca03-2e25-49ce-a9fc-62650dbefb29", null, "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6574cd2a-03bc-4f6d-a837-15dc25b6e510", "AQAAAAIAAYagAAAAEB5Za4HHJy2cWj2O7gMUD3yfST4+JJQQpmalqx+qhlYaAmzGL2CyD3g2aMVsyKlCdQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cd0ca03-2e25-49ce-a9fc-62650dbefb29");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "SubProduct");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e841caba-2e08-404e-a239-107f66a9cd28", null, "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "38e7d388-d59b-4558-b8d5-9f105d670715", "AQAAAAIAAYagAAAAEJ33veNI89O8EAHf+u2eHL8oc0YUwCxOvqnnKuyQFZZiJLIVb6jXWzrK2HYYOg/Qyg==" });
        }
    }
}
