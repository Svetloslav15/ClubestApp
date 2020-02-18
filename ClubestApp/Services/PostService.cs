namespace ClubestApp.Services
{
    using ClubestApp.Data;
    using ClubestApp.Data.Models;
    using ClubestApp.Data.Models.Enums;
    using ClubestApp.Models.InputModels.Posts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class PostService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly CloudinaryService cloudinaryService;
        private readonly UserService userService;

        public PostService(ApplicationDbContext dbContext,
            CloudinaryService cloudinaryService,
            UserService userService)
        {
            this.dbContext = dbContext;
            this.cloudinaryService = cloudinaryService;
            this.userService = userService;
        }

        private bool CheckIsLink(string content)
        {
            string pattern = "";
            Regex regex = new Regex(pattern);

            if (regex.IsMatch(content))
            {
                return true;
            }

            return false;
        }

        public async Task<Post> GetPostById(string postId)
        {
            return await this.dbContext.Posts.FirstOrDefaultAsync(post => post.Id == postId);
        }

        public async Task<Post> AddPost(AddPostInputModel inputModel)
        {
            string secondaryContent = "";
            PostType postType = PostType.Text;

            if (inputModel.FormFile != null)
            {
                secondaryContent = await this.cloudinaryService.UploadImage(inputModel.FormFile);
                postType = PostType.Image;
            }

            Post post = new Post()
            {
                Content = inputModel.Content,
                Author = inputModel.User,
                AuthorId = inputModel.User.Id,
                ClubId = inputModel.Club.Id,
                Club = inputModel.Club,
                DateTime = DateTime.Now,
                FileUrlOrLink = secondaryContent,
                PostType = postType
            };

            await this.dbContext.Posts.AddAsync(post);
            await this.dbContext.SaveChangesAsync();

            return post;
        }

        public async Task<IList<Post>> GetAllPostsForClub(string clubId)
        {
            return await this.dbContext.Posts
                .Where(post => post.ClubId == clubId)
                .Include(post => post.Author)
                .OrderByDescending(post => post.DateTime)
                .ToListAsync();
        }

        public async Task<UserPostLikes> LikePost(string postId, string userId)
        {
            Post post = await this.GetPostById(postId);
            User user = await this.userService.FindUserById(userId);

            UserPostLikes userPostLikes = new UserPostLikes()
            {
                User = user,
                UserId = userId,
                Post = post,
                PostId = postId
            };

            await this.dbContext.UserPostLikes.AddAsync(userPostLikes);
            await this.dbContext.SaveChangesAsync();

            return userPostLikes;
        }

        public async Task<UserPostDislikes> DislikePost(string postId, string userId)
        {
            Post post = await this.GetPostById(postId);
            User user = await this.userService.FindUserById(userId);

            UserPostDislikes userPostDislikes = new UserPostDislikes()
            {
                User = user,
                UserId = userId,
                Post = post,
                PostId = postId
            };

            await this.dbContext.UserPostDislikes.AddAsync(userPostDislikes);
            await this.dbContext.SaveChangesAsync();

            return userPostDislikes;
        }

    }
}