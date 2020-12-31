using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModelLibrary.Migrations.User
{
    public partial class RecoveryUserDbAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RecoveryDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RecoveryPassword",
                table: "Users",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecoveryDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RecoveryPassword",
                table: "Users");
        }
    }
}
