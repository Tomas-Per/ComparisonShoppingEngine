using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModelLibrary.Migrations.Smartphone
{
    public partial class SmarphoneDBCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Smartphones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrontCameras = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    BackCameras = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ScreenDiagonal = table.Column<double>(type: "float", nullable: false),
                    Storage = table.Column<int>(type: "int", nullable: false),
                    RAM = table.Column<int>(type: "int", nullable: false),
                    Processor = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Resolution = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    BatteryStorage = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<int>(type: "int", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ManufacturerName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ItemURL = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    ShopName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    ImageLink = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ItemCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smartphones", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Smartphones");
        }
    }
}
