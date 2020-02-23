namespace ClubestApp.Models.InputModels
{
    using ClubestApp.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddPollInputModel
    {
        [Required(ErrorMessage = ErrorMessages.PollContentRequired)]
        public string Content { get; set; }

        public string Options { get; set; }

        public string IsMultichoice { get; set; }

        public DateTime ExpiredDate { get; set; }

        public string ClubId { get; set; }

        public List<string> Votes { get; set; }

        public string PollId { get; set; }
    }
}
