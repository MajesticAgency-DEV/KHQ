using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KHQ.Repo.Migrations
{
    /// <inheritdoc />
    public partial class editbrochures01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1381c376-a5cb-4cf8-a1a6-4c2269ef3df4");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Brouchures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5fa121e2-3458-431d-94b0-50d845c5e15f", null, "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "28916341-37b3-4eb5-bdbe-d88e8129fa52", "AQAAAAIAAYagAAAAELTUi1LE8QoteShGEAFLrgO1kzjcI/rDc++79TiDtASXEQKl+cf03/NboOFfc8u2qA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5fa121e2-3458-431d-94b0-50d845c5e15f");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Brouchures");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1381c376-a5cb-4cf8-a1a6-4c2269ef3df4", null, "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b10300bf-5acf-4b62-9166-5d00d1728ddf", "AQAAAAIAAYagAAAAEKrDQ7J8epexpPDLgnRP6Q++jsBtsmck0n7w5verRaIMP80NgCK6OKdEe2+yqLwyrw==" });
        }
    }
}
