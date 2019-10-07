namespace Clubest.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Option
    {
        public Option()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string Content { get; set; }

        public string PollId { get; set; }

        public Poll Poll { get; set; }
    }
}