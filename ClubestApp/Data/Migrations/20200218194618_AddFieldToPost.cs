using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubestApp.Migrations
{
    public partial class AddFieldToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileUrlOrLink",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUrlOrLink",
                table: "Posts");
        }
    }
}
