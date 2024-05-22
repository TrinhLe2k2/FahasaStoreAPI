using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Services
{
    public interface IImageUploader
    {
        public Task<UploadResult> UploadImageAsync(IFormFile file);
    }
    public class ImageUploader : IImageUploader
    {
        private readonly Cloudinary _cloudinary;
        public ImageUploader(IConfiguration configuration)
        {
            var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<UploadResult> UploadImageAsync(IFormFile file)
        {
            using (var stream = file?.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file?.FileName, stream),
                    // You can add more parameters here, like folder, tags, etc.
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult;
            }
        }
    }
}
