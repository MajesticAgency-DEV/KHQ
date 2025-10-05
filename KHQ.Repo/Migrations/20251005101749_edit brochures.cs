using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KHQ.Repo.Migrations
{
    /// <inheritdoc />
    public partial class editbrochures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cd0ca03-2e25-49ce-a9fc-62650dbefb29");

            migrationBuilder.DropColumn(
                name: "DescriptionAr",
                table: "Brouchures");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "Brouchures");

            migrationBuilder.DropColumn(
                name: "TitleAr",
                table: "Brouchures");

            migrationBuilder.DropColumn(
                name: "TitleEn",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1381c376-a5cb-4cf8-a1a6-4c2269ef3df4");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAr",
                table: "Brouchures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "Brouchures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleAr",
                table: "Brouchures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                table: "Brouchures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
    }
}
