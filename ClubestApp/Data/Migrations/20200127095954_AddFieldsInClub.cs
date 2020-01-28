using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubestApp.Migrations
{
    public partial class AddFieldsInClub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Interests",
                table: "Clubs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "Clubs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interests",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "Town",
                table: "Clubs");
        }
    }
}
