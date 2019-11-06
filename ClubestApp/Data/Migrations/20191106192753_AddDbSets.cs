using Microsoft.EntityFrameworkCore.Migrations;

namespace ClubestApp.Data.Migrations
{
    public partial class AddDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubAdmin_Club_ClubId",
                table: "ClubAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubAdmin_AspNetUsers_UserId",
                table: "ClubAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_AuthorId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_AspNetUsers_AdminId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Club_ClubId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_EventUser_Event_EventId",
                table: "EventUser");

            migrationBuilder.DropForeignKey(
                name: "FK_EventUser_AspNetUsers_UserId",
                table: "EventUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_UserId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Option_Poll_PollId",
                table: "Option");

            migrationBuilder.DropForeignKey(
                name: "FK_Poll_Club_ClubId",
                table: "Poll");

            migrationBuilder.DropForeignKey(
                name: "FK_PollUser_Poll_PollId",
                table: "PollUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PollVotedUsers_Poll_PollId",
                table: "PollVotedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_AspNetUsers_AuthorId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Club_ClubId",
                table: "Post");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestNewClub_AspNetUsers_AuthorId",
                table: "RequestNewClub");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClub_Club_ClubId",
                table: "UserClub");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClub_AspNetUsers_UserId",
                table: "UserClub");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCommentDislikes_Comment_CommentId",
                table: "UserCommentDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCommentLikes_Comment_CommentId",
                table: "UserCommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPostDislikes_Post_PostId",
                table: "UserPostDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPostLikes_Post_PostId",
                table: "UserPostLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClub",
                table: "UserClub");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestNewClub",
                table: "RequestNewClub");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Poll",
                table: "Poll");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Option",
                table: "Option");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventUser",
                table: "EventUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubAdmin",
                table: "ClubAdmin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Club",
                table: "Club");

            migrationBuilder.RenameTable(
                name: "UserClub",
                newName: "UserClubs");

            migrationBuilder.RenameTable(
                name: "RequestNewClub",
                newName: "RequestNewClubs");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "Poll",
                newName: "Polls");

            migrationBuilder.RenameTable(
                name: "Option",
                newName: "Options");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "EventUser",
                newName: "EventUsers");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameTable(
                name: "ClubAdmin",
                newName: "ClubAdmins");

            migrationBuilder.RenameTable(
                name: "Club",
                newName: "Clubs");

            migrationBuilder.RenameIndex(
                name: "IX_UserClub_ClubId",
                table: "UserClubs",
                newName: "IX_UserClubs_ClubId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestNewClub_AuthorId",
                table: "RequestNewClubs",
                newName: "IX_RequestNewClubs_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_ClubId",
                table: "Posts",
                newName: "IX_Posts_ClubId");

            migrationBuilder.RenameIndex(
                name: "IX_Post_AuthorId",
                table: "Posts",
                newName: "IX_Posts_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Poll_ClubId",
                table: "Polls",
                newName: "IX_Polls_ClubId");

            migrationBuilder.RenameIndex(
                name: "IX_Option_PollId",
                table: "Options",
                newName: "IX_Options_PollId");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_UserId",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EventUser_EventId",
                table: "EventUsers",
                newName: "IX_EventUsers_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_ClubId",
                table: "Events",
                newName: "IX_Events_ClubId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_AdminId",
                table: "Events",
                newName: "IX_Events_AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_PostId",
                table: "Comments",
                newName: "IX_Comments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_AuthorId",
                table: "Comments",
                newName: "IX_Comments_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_ClubAdmin_ClubId",
                table: "ClubAdmins",
                newName: "IX_ClubAdmins_ClubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClubs",
                table: "UserClubs",
                columns: new[] { "UserId", "ClubId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestNewClubs",
                table: "RequestNewClubs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Polls",
                table: "Polls",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Options",
                table: "Options",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventUsers",
                table: "EventUsers",
                columns: new[] { "UserId", "EventId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubAdmins",
                table: "ClubAdmins",
                columns: new[] { "UserId", "ClubId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clubs",
                table: "Clubs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubAdmins_Clubs_ClubId",
                table: "ClubAdmins",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubAdmins_AspNetUsers_UserId",
                table: "ClubAdmins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_AdminId",
                table: "Events",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Clubs_ClubId",
                table: "Events",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventUsers_Events_EventId",
                table: "EventUsers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventUsers_AspNetUsers_UserId",
                table: "EventUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Polls_PollId",
                table: "Options",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Polls_Clubs_ClubId",
                table: "Polls",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PollUser_Polls_PollId",
                table: "PollUser",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PollVotedUsers_Polls_PollId",
                table: "PollVotedUsers",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Clubs_ClubId",
                table: "Posts",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestNewClubs_AspNetUsers_AuthorId",
                table: "RequestNewClubs",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClubs_Clubs_ClubId",
                table: "UserClubs",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClubs_AspNetUsers_UserId",
                table: "UserClubs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommentDislikes_Comments_CommentId",
                table: "UserCommentDislikes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommentLikes_Comments_CommentId",
                table: "UserCommentLikes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPostDislikes_Posts_PostId",
                table: "UserPostDislikes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPostLikes_Posts_PostId",
                table: "UserPostLikes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubAdmins_Clubs_ClubId",
                table: "ClubAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubAdmins_AspNetUsers_UserId",
                table: "ClubAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_AdminId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Clubs_ClubId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_EventUsers_Events_EventId",
                table: "EventUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_EventUsers_AspNetUsers_UserId",
                table: "EventUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Options_Polls_PollId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Polls_Clubs_ClubId",
                table: "Polls");

            migrationBuilder.DropForeignKey(
                name: "FK_PollUser_Polls_PollId",
                table: "PollUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PollVotedUsers_Polls_PollId",
                table: "PollVotedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AuthorId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Clubs_ClubId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestNewClubs_AspNetUsers_AuthorId",
                table: "RequestNewClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClubs_Clubs_ClubId",
                table: "UserClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClubs_AspNetUsers_UserId",
                table: "UserClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCommentDislikes_Comments_CommentId",
                table: "UserCommentDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCommentLikes_Comments_CommentId",
                table: "UserCommentLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPostDislikes_Posts_PostId",
                table: "UserPostDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPostLikes_Posts_PostId",
                table: "UserPostLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClubs",
                table: "UserClubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestNewClubs",
                table: "RequestNewClubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Polls",
                table: "Polls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Options",
                table: "Options");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventUsers",
                table: "EventUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clubs",
                table: "Clubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubAdmins",
                table: "ClubAdmins");

            migrationBuilder.RenameTable(
                name: "UserClubs",
                newName: "UserClub");

            migrationBuilder.RenameTable(
                name: "RequestNewClubs",
                newName: "RequestNewClub");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.RenameTable(
                name: "Polls",
                newName: "Poll");

            migrationBuilder.RenameTable(
                name: "Options",
                newName: "Option");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Notification");

            migrationBuilder.RenameTable(
                name: "EventUsers",
                newName: "EventUser");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameTable(
                name: "Clubs",
                newName: "Club");

            migrationBuilder.RenameTable(
                name: "ClubAdmins",
                newName: "ClubAdmin");

            migrationBuilder.RenameIndex(
                name: "IX_UserClubs_ClubId",
                table: "UserClub",
                newName: "IX_UserClub_ClubId");

            migrationBuilder.RenameIndex(
                name: "IX_RequestNewClubs_AuthorId",
                table: "RequestNewClub",
                newName: "IX_RequestNewClub_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_ClubId",
                table: "Post",
                newName: "IX_Post_ClubId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_AuthorId",
                table: "Post",
                newName: "IX_Post_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Polls_ClubId",
                table: "Poll",
                newName: "IX_Poll_ClubId");

            migrationBuilder.RenameIndex(
                name: "IX_Options_PollId",
                table: "Option",
                newName: "IX_Option_PollId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "Notification",
                newName: "IX_Notification_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EventUsers_EventId",
                table: "EventUser",
                newName: "IX_EventUser_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_ClubId",
                table: "Event",
                newName: "IX_Event_ClubId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_AdminId",
                table: "Event",
                newName: "IX_Event_AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PostId",
                table: "Comment",
                newName: "IX_Comment_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AuthorId",
                table: "Comment",
                newName: "IX_Comment_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_ClubAdmins_ClubId",
                table: "ClubAdmin",
                newName: "IX_ClubAdmin_ClubId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClub",
                table: "UserClub",
                columns: new[] { "UserId", "ClubId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestNewClub",
                table: "RequestNewClub",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Poll",
                table: "Poll",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Option",
                table: "Option",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventUser",
                table: "EventUser",
                columns: new[] { "UserId", "EventId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Club",
                table: "Club",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubAdmin",
                table: "ClubAdmin",
                columns: new[] { "UserId", "ClubId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ClubAdmin_Club_ClubId",
                table: "ClubAdmin",
                column: "ClubId",
                principalTable: "Club",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubAdmin_AspNetUsers_UserId",
                table: "ClubAdmin",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_AspNetUsers_AdminId",
                table: "Event",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Club_ClubId",
                table: "Event",
                column: "ClubId",
                principalTable: "Club",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventUser_Event_EventId",
                table: "EventUser",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventUser_AspNetUsers_UserId",
                table: "EventUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_UserId",
                table: "Notification",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Option_Poll_PollId",
                table: "Option",
                column: "PollId",
                principalTable: "Poll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Poll_Club_ClubId",
                table: "Poll",
                column: "ClubId",
                principalTable: "Club",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PollUser_Poll_PollId",
                table: "PollUser",
                column: "PollId",
                principalTable: "Poll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PollVotedUsers_Poll_PollId",
                table: "PollVotedUsers",
                column: "PollId",
                principalTable: "Poll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_AspNetUsers_AuthorId",
                table: "Post",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Club_ClubId",
                table: "Post",
                column: "ClubId",
                principalTable: "Club",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestNewClub_AspNetUsers_AuthorId",
                table: "RequestNewClub",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClub_Club_ClubId",
                table: "UserClub",
                column: "ClubId",
                principalTable: "Club",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClub_AspNetUsers_UserId",
                table: "UserClub",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommentDislikes_Comment_CommentId",
                table: "UserCommentDislikes",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCommentLikes_Comment_CommentId",
                table: "UserCommentLikes",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPostDislikes_Post_PostId",
                table: "UserPostDislikes",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPostLikes_Post_PostId",
                table: "UserPostLikes",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
