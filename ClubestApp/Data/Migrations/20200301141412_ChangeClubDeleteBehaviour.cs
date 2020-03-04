using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubestApp.Data.Migrations
{
    public partial class ChangeClubDeleteBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JoinClubRequests_Clubs_ClubId",
                table: "JoinClubRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_JoinClubRequests_Clubs_ClubId",
                table: "JoinClubRequests",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JoinClubRequests_Clubs_ClubId",
                table: "JoinClubRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_JoinClubRequests_Clubs_ClubId",
                table: "JoinClubRequests",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
