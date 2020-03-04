namespace ClubestApp.Models.BindingModels
{
    using ClubestApp.Data.Models.Enums;

    public class RequestApproveBindingModel
    {
        public string ClubId { get; set; }

        public string RequestId { get; set; }

        public int RequestType { get; set; }
    }
}