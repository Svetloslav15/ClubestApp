using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubestApp.Migrations
{
    public partial class AddUserTown : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Town",
                table: "AspNetUsers");
        }
    }
}
