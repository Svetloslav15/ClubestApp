namespace ClubestApp.Services.Contracts
{
    using ClubestApp.Data.Models;
    using ClubestApp.Models.InputModels.Comments;
    
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task<Comment> AddComment(AddCommentInputModel inputModel);
    }
}