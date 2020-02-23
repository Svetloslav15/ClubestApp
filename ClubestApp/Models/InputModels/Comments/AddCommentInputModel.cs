namespace ClubestApp.Models.InputModels.Comments
{
    using ClubestApp.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class AddCommentInputModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string PostId { get; set; }

        public string ClubId { get; set; }

        public User Author { get; set; }
    }
}