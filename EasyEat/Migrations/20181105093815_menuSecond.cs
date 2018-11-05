using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyEat.Migrations
{
    public partial class menuSecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartPart_DishID",
                table: "CartPart");

            migrationBuilder.RenameColumn(
                name: "DishID",
                table: "CartPart",
                newName: "MenuID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartPart_MenuID",
                table: "CartPart",
                column: "MenuID",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartPart_MenuID",
                table: "CartPart");

            migrationBuilder.RenameColumn(
                name: "MenuID",
                table: "CartPart",
                newName: "DishID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartPart_DishID",
                table: "CartPart",
                column: "DishID",
                principalTable: "Dish",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
