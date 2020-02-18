namespace ClubestApp.Models.InputModels.Posts
{
    using ClubestApp.Data.Models;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class AddPostInputModel
    {
        [Required]
        [MaxLength(400)]
        public string Content { get; set; }
        
        public IFormFile FormFile { get; set; }

        public User User { get; set; }

        public Club Club { get; set; }

        public string ClubId { get; set; }
    }
}