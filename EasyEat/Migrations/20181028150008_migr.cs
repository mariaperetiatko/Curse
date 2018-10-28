using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyEat.Migrations
{
    public partial class migr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "Customer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IdentityId",
                table: "Customer",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_AspNetUsers_IdentityId",
                table: "Customer",
                column: "IdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_AspNetUsers_IdentityId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_IdentityId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Customer");
        }
    }
}
