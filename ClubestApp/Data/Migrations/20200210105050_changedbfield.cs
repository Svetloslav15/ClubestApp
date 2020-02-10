using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubestApp.Migrations
{
    public partial class changedbfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "JoinClubRequests");

            migrationBuilder.AddColumn<int>(
                name: "RequestType",
                table: "JoinClubRequests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestType",
                table: "JoinClubRequests");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "JoinClubRequests",
                nullable: false,
                defaultValue: false);
        }
    }
}
