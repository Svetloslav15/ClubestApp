using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubestApp.Data.Migrations
{
    public partial class AddClubField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Description",
                table: "Clubs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Clubs");
        }
    }
}
