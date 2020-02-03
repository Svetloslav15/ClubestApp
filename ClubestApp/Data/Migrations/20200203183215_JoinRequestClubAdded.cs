using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubestApp.Migrations
{
    public partial class JoinRequestClubAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JoinClubRequests",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    ClubId = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinClubRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JoinClubRequests_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JoinClubRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JoinClubRequests_ClubId",
                table: "JoinClubRequests",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinClubRequests_UserId",
                table: "JoinClubRequests",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JoinClubRequests");
        }
    }
}
