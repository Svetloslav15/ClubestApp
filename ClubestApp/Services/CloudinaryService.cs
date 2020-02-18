namespace ClubestApp.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using System.IO;
    using System.Threading.Tasks;

    public class CloudinaryService
    {
        private readonly Cloudinary cloudinary;
        private readonly IConfiguration configuration;

        public CloudinaryService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.cloudinary = new Cloudinary(
                                        new Account(
                                             this.configuration.GetConnectionString("CloudinaryCloudName"),
                                             this.configuration.GetConnectionString("CloudinaryApiKey"),
                                             this.configuration.GetConnectionString("CloudinaryAppSecret"))
                                        );
        }

        public async Task<string> UploadImage(IFormFile photoFile)
        {
            //Work on image
            string currentUrl = "";
            if (photoFile != null)
            {
                string filePath = Path.GetFileName(photoFile.FileName);
                using (var stream = File.Create(filePath))
                {
                    photoFile.CopyToAsync(stream)
                        .GetAwaiter()
                        .GetResult();
                }

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(filePath),
                    PublicId = $"ClubestPics/{photoFile.FileName}"
                };

                //Deletes file in pc and uploads it in cloud
                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                File.Delete(filePath);
                currentUrl = uploadResult.Uri.AbsoluteUri;
            }

            return currentUrl;
        }
    }
}