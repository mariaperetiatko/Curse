using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyEat.Migrations
{
    public partial class BoxMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                        migrationBuilder.CreateTable(
                name: "BoxMigration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Latitude = table.Column<double>(nullable: false),
                    Longtitude = table.Column<double>(nullable: false),
                    Moment = table.Column<DateTime>(nullable: false),
                    FoodOrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxMigration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoxMigration_FoodOrderID",
                        column: x => x.FoodOrderId,
                        principalTable: "FoodOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoxMigration_FoodOrderId",
                table: "BoxMigration",
                column: "FoodOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxMigration");

            }
    }
}
