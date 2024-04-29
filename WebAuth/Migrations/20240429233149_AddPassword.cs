using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAuth.Migrations
{
    /// <inheritdoc />
    public partial class AddPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_Id",
                keyValue: new Guid("5e68b298-2670-4252-a8f3-7ab8409fa540"));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_Id", "FirstName", "LastName", "Password", "UserName" },
                values: new object[] { new Guid("c846a7f8-3718-4dde-abf0-0b11d42cfd24"), "Anand", "Shah", null, "anandshah" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "User_Id",
                keyValue: new Guid("c846a7f8-3718-4dde-abf0-0b11d42cfd24"));

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_Id", "FirstName", "LastName", "UserName" },
                values: new object[] { new Guid("5e68b298-2670-4252-a8f3-7ab8409fa540"), "Anand", "Shah", "anandshah" });
        }
    }
}
