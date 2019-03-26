using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyEat.Migrations
{
    public partial class BoxTemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Temperature",
                table: "BoxMigration",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "BoxMigration");
        }
    }
}
