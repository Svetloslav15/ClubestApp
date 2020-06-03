namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels.Comments;
    using ClubestApp.Services.Contracts;

    using System;
    using System.Threading.Tasks;

    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly PostService postService;

        public CommentService(ApplicationDbContext dbContext,
            PostService postService)
        {
            this.dbContext = dbContext;
            this.postService = postService;
        }

        public async Task<Comment> AddComment(AddCommentInputModel inputModel)
        {
            Post post = await this.postService.GetPostById(inputModel.PostId);

            Comment comment = new Comment()
            {
                Content = inputModel.Content,
                Author = inputModel.Author,
                AuthorId = inputModel.Author.Id,
                Post = post,
                PostId = post.Id,
                Date = DateTime.Now
            };

            await this.dbContext.Comments.AddAsync(comment);
            await this.dbContext.SaveChangesAsync();

            return comment;
        }
    }
}