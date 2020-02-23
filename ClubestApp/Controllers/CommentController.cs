namespace ClubestApp.Controllers
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels.Comments;
    using ClubestApp.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CommentController : Controller
    {
        private readonly CommentService commentService;
        private readonly UserManager<User> userManager;

        public CommentController(CommentService commentService,
             UserManager<User> userManager)
        {
            this.commentService = commentService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCommentInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                User user = await this.userManager.GetUserAsync(User);
                inputModel.Author = user;
                await this.commentService.AddComment(inputModel);
            }
            return this.Redirect($"/Club/Details/{inputModel.ClubId}#{inputModel.PostId}");
        }
    }
}