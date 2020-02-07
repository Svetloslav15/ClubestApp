using Microsoft.AspNetCore.Http;

namespace ClubestApp.Models.InputModels
{
    public class ChangePhotoInputModel
    {
        public IFormFile PhotoFile { get; set; }
    }
}
