using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubestApp.Data.Migrations
{
    public partial class AddPropertyToMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Message",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Message");
        }
    }
}
