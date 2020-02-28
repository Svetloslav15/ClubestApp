using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubestApp.Migrations
{
    public partial class addnewfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "RequestNewClubs",
                newName: "Town");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RequestNewClubs",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "RequestNewClubs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Interests",
                table: "RequestNewClubs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "RequestNewClubs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RequestNewClubs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "RequestNewClubs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "RequestNewClubs");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "RequestNewClubs");

            migrationBuilder.DropColumn(
                name: "Interests",
                table: "RequestNewClubs");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "RequestNewClubs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RequestNewClubs");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "RequestNewClubs");

            migrationBuilder.RenameColumn(
                name: "Town",
                table: "RequestNewClubs",
                newName: "Content");
        }
    }
}
