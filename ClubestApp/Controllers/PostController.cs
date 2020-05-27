namespace ClubestApp.Controllers
{
    using ClubestApp.Common;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels.Posts;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PostController : Controller
    {
        private readonly PostService postService;
        private readonly UserManager<User> userManager;
        private readonly ClubService clubService;

        public PostController(PostService postService,
            ClubService clubService,
            UserManager<User> userManager)
        {
            this.postService = postService;
            this.clubService = clubService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPost(AddPostInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                User user = await this.userManager.GetUserAsync(User);
                inputModel.User = user;
                Club club = await this.clubService.GetClubById(inputModel.ClubId);
                inputModel.Club = club;
                await this.postService.AddPost(inputModel);
            }
            return this.Redirect($"/Club/Details/{inputModel.ClubId}");
        }

        [Authorize]
        public async Task<IActionResult> Like(string id)
        {
            User user = await this.userManager.GetUserAsync(User);
            var post = await this.postService.LikePost(id, user);

            var list = new List<int>()
            { 
                post.UserPostLikes.Count,
                post.UserPostDislikes.Count 
            };
            return Ok(list);
        }

        [Authorize]
        public async Task<IActionResult> Dislike(string id)
        {
            User user = await this.userManager.GetUserAsync(User);
            var post = await this.postService.DislikePost(id, user);

            var list = new List<int>()
            {
                post.UserPostLikes.Count,
                post.UserPostDislikes.Count
            };
            return Ok(list);
        }

        [Authorize(Roles = UserRoles.SystemOrClubAdmin)]
        public async Task<IActionResult> Delete(string id, [FromQuery] string clubId)
        {
            await this.postService.DeletePost(id);

            return this.Redirect($"/Club/Details/{clubId}");
        }
    }
}