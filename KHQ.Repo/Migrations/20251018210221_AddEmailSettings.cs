using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KHQ.Repo.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de38601a-9d6a-4ff9-b30f-42ebb1354ca5");

            migrationBuilder.CreateTable(
                name: "EmailSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Regards = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupportTeam = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.Id);
                });

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[] { "9621c564-d946-4c30-97c1-83af84b42cec", null, "User", "User" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "a18be9c0",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash" },
            //    values: new object[] { "9fefe49c-116f-487f-9cdc-ecb697bbbeff", "AQAAAAIAAYagAAAAEAAnXhDBvFgQl1rxRnm0aSOHbC6Pis3F13hsNmuT5nIq6OJgEuKrJnuc/ypDfKtPdA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailSettings");

            //migrationBuilder.DeleteData(
            //    table: "AspNetRoles",
            //    keyColumn: "Id",
            //    keyValue: "9621c564-d946-4c30-97c1-83af84b42cec");

            //migrationBuilder.InsertData(
            //    table: "AspNetRoles",
            //    columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            //    values: new object[] { "de38601a-9d6a-4ff9-b30f-42ebb1354ca5", null, "User", "User" });

            //migrationBuilder.UpdateData(
            //    table: "AspNetUsers",
            //    keyColumn: "Id",
            //    keyValue: "a18be9c0",
            //    columns: new[] { "ConcurrencyStamp", "PasswordHash" },
            //    values: new object[] { "86f2857c-0c14-4211-95ba-4f8375a01025", "AQAAAAIAAYagAAAAEIelg3Y4RPOS0kAS+ics0GMeYMN5ijFXR0asaasmNPwt3pKyUYQUnqvOKMu2Is34ug==" });
        }
    }
}
