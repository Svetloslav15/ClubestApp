namespace ClubestApp.Services.Contracts
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public interface ICloudinaryService
    {
        Task<string> UploadImage(IFormFile photoFile);
    }
}