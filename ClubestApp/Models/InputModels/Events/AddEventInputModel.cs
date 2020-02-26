namespace ClubestApp.Models.InputModels.Events
{
    using ClubestApp.Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddEventInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ClubId { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public string Interests { get; set; }
    }
}