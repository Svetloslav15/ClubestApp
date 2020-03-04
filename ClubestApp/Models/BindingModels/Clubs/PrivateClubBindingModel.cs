namespace ClubestApp.Models.BindingModels
{
    using ClubestApp.Data.Models.Enums;

    public class PrivateClubBindingModel
    {
        public string Name { get; set; }

        public decimal Fee { get; set; }

        public string Description { get; set; }

        public string Town { get; set; }

        public string Interests { get; set; }

        public string PictureUrl { get; set; }

        public PriceType PriceType { get; set; }
    }
}
