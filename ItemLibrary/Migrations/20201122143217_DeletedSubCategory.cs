using Microsoft.EntityFrameworkCore.Migrations;

namespace ItemLibrary.Migrations
{
    public partial class DeletedSubCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComputerCategory",
                table: "Computers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComputerCategory",
                table: "Computers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
