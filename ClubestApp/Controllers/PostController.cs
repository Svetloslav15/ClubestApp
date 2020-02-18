namespace ClubestApp.Controllers
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels.Posts;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
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
    }
}