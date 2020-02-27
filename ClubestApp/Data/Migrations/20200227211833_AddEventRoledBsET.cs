using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubestApp.Data.Migrations
{
    public partial class AddEventRoledBsET : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRole_Events_EventId",
                table: "EventRole");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRole_AspNetUsers_UserId",
                table: "EventRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventRole",
                table: "EventRole");

            migrationBuilder.RenameTable(
                name: "EventRole",
                newName: "EventRoles");

            migrationBuilder.RenameIndex(
                name: "IX_EventRole_UserId",
                table: "EventRoles",
                newName: "IX_EventRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EventRole_EventId",
                table: "EventRoles",
                newName: "IX_EventRoles_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventRoles",
                table: "EventRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRoles_Events_EventId",
                table: "EventRoles",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRoles_AspNetUsers_UserId",
                table: "EventRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventRoles_Events_EventId",
                table: "EventRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRoles_AspNetUsers_UserId",
                table: "EventRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventRoles",
                table: "EventRoles");

            migrationBuilder.RenameTable(
                name: "EventRoles",
                newName: "EventRole");

            migrationBuilder.RenameIndex(
                name: "IX_EventRoles_UserId",
                table: "EventRole",
                newName: "IX_EventRole_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EventRoles_EventId",
                table: "EventRole",
                newName: "IX_EventRole_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventRole",
                table: "EventRole",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventRole_Events_EventId",
                table: "EventRole",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRole_AspNetUsers_UserId",
                table: "EventRole",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
