using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModelLibrary.Migrations.User
{
    public partial class ReviewsAndFavoriteDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ItemCategory = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<int>(type: "int", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ManufacturerName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    ItemURL = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    ShopName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    ImageLink = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => new { x.Id, x.ItemCategory });
                });

            migrationBuilder.CreateTable(
                name: "FavoriteItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    ItemCategory = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteItems_Items_ItemId_ItemCategory",
                        columns: x => new { x.ItemId, x.ItemCategory },
                        principalTable: "Items",
                        principalColumns: new[] { "Id", "ItemCategory" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(280)", maxLength: 280, nullable: true),
                    Score = table.Column<double>(type: "float", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    ItemCategory = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Items_ItemId_ItemCategory",
                        columns: x => new { x.ItemId, x.ItemCategory },
                        principalTable: "Items",
                        principalColumns: new[] { "Id", "ItemCategory" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteItems_ItemId_ItemCategory",
                table: "FavoriteItems",
                columns: new[] { "ItemId", "ItemCategory" });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteItems_UserId",
                table: "FavoriteItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ItemId_ItemCategory",
                table: "Reviews",
                columns: new[] { "ItemId", "ItemCategory" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteItems");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
