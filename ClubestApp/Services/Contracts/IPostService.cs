namespace ClubestApp.Services.Contracts
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels.Posts;
    
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostService
    {
        Task<Post> GetPostById(string postId);

        Task<Post> AddPost(AddPostInputModel inputModel);

        Task<IList<Post>> GetAllPostsForClub(string clubId);

        Task<Post> LikePost(string postId, User user);

        Task<Post> DislikePost(string postId, User user);

        Task<Post> DeletePost(string postId);

        Task<IList<Post>> GetPostsForHomePage(string userId);
    }
}