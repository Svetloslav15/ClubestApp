namespace ClubestApp.Models.BindingModels
{
    public class GetClubsBindingModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Town { get; set; }

        public string PictureUrl { get; set; }

        public bool IsPublic { get; set; }
    }
}
