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
            Post postEntity = await this.dbContext.Posts
                .Include(post => post.UserPostLikes)
                .Include(post => post.UserPostDislikes)
                .Include(post => post.Comments)
                    .ThenInclude(comment => comment.UserCommentDislikes)
                .Include(post => post.Comments)
                    .ThenInclude(comment => comment.UserCommentLikes)
                .FirstOrDefaultAsync(post => post.Id == postId);

            postEntity.Comments = postEntity.Comments
                .OrderBy(comment => comment.Date)
                .ToList();

            return postEntity;
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
            IList<Post> posts = await this.dbContext.Posts
                .Where(post => post.ClubId == clubId)
                .Include(post => post.Author)
                .Include(post => post.UserPostLikes)
                .Include(post => post.UserPostDislikes)
                .Include(post => post.Comments)
                .OrderByDescending(post => post.DateTime)
                .ToListAsync();

            foreach (Post post in posts)
            {
                post.Comments = post.Comments
                    .OrderByDescending(comment => comment.Date)
                    .ToList();
            }

            return posts;
        }

        public async Task<Post> LikePost(string postId, User user)
        {
            Post post = await this.GetPostById(postId);
            if (!post.UserPostLikes.Any(x => x.UserId == user.Id))
            {
                UserPostLikes userPostLikes = new UserPostLikes()
                {
                    User = user,
                    UserId = user.Id,
                    Post = post,
                    PostId = postId
                };

                await this.dbContext.UserPostLikes.AddAsync(userPostLikes);
            }
            if (post.UserPostDislikes.Any(x => x.UserId == user.Id))
            {
                UserPostDislikes entity = await this.dbContext.UserPostDislikes.FirstOrDefaultAsync(x => x.UserId == user.Id);
                this.dbContext.UserPostDislikes.Remove(entity);
            }

            await this.dbContext.SaveChangesAsync();

            return post;
        }

        public async Task<Post> DislikePost(string postId, User user)
        {
            Post post = await this.GetPostById(postId);

            if (!post.UserPostDislikes.Any(x => x.UserId == user.Id))
            {
                UserPostDislikes userPostDislikes = new UserPostDislikes()
                {
                    User = user,
                    UserId = user.Id,
                    Post = post,
                    PostId = postId
                };

                await this.dbContext.UserPostDislikes.AddAsync(userPostDislikes);
            }
            if (post.UserPostLikes.Any(x => x.UserId == user.Id))
            {
                UserPostLikes entity = await this.dbContext.UserPostLikes.FirstOrDefaultAsync(x => x.UserId == user.Id);
                this.dbContext.UserPostLikes.Remove(entity);
            }
            
            await this.dbContext.SaveChangesAsync();

            return post;
        }

        public async Task<Post> DeletePost(string postId)
        {
            Post post = await this.GetPostById(postId);
            this.dbContext.Posts.Remove(post);

            await this.dbContext.SaveChangesAsync();

            return post;
        }

        public async Task<List<Post>> GetPostsForHomePage(string userId)
        {
            List<Club> userClubs = await this.dbContext.UserClubs
                .Where(x => x.UserId == userId)
                .Select(x => x.Club)
                .ToListAsync();

            List<Post> posts = await this.dbContext.Posts
                .Include(x => x.Author)
                .Include(x => x.Comments)
                .Include(x => x.UserPostDislikes)
                .Include(x => x.UserPostLikes)
                .Where(x => userClubs.Any(y => y.Id == x.ClubId))
                .ToListAsync();

            return posts;
        }
    }
}