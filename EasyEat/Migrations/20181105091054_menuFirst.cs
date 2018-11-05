using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyEat.Migrations
{
    public partial class menuFirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_Menu",
                table: "Menu");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Menu",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu",
                table: "Menu",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_DishID",
                table: "Menu",
                column: "DishID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu",
                table: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Menu_DishID",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Menu");

            migrationBuilder.AddPrimaryKey(
                name: "pk_Menu",
                table: "Menu",
                columns: new[] { "DishID", "RestaurantID" });
        }
    }
}
