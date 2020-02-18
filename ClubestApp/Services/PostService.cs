namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels.Posts;
    using System;
    using System.Threading.Tasks;

    public class PostService
    {
        private readonly ApplicationDbContext dbContext;

        public PostService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Post> AddPost(AddPostInputModel inputModel)
        {
            Post post = new Post()
            {
                Content = inputModel.Content,
                Author = inputModel.User,
                AuthorId = inputModel.User.Id,
                ClubId = inputModel.Club.Id,
                Club = inputModel.Club,
                DateTime = DateTime.Now
            };

            await this.dbContext.Posts.AddAsync(post);
            await this.dbContext.SaveChangesAsync();

            return post;
        }
    }
}