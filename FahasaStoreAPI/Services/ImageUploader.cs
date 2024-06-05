using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;

namespace FahasaStoreAPI.Services
{
    public interface IImageUploader
    {
        public Task<ImageUploadResultModel> UploadImageAsync(IFormFile file, string? folderName);
        public Task<bool> RemoveImageAsync(string? publicId);
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

        public async Task<bool> RemoveImageAsync(string? publicId)
        {
            if (string.IsNullOrWhiteSpace(publicId))
            {
                return false;
            }
            var deletionParams = new DeletionParams(publicId);
            var deletionResult = await _cloudinary.DestroyAsync(deletionParams);
            return deletionResult.Result == "ok";
        }

        public async Task<ImageUploadResultModel> UploadImageAsync(IFormFile file, string? folderName)
        {
            if (file == null || file.Length == 0)
            {
                var imageUploadResult = new ImageUploadResultModel("https://res-console.cloudinary.com/drk83zqgs/media_explorer_thumbnails/6d6a5b0e8c5f1954f29b609202821745/detailed", null);
                return imageUploadResult;
            }

            using (var stream = file.OpenReadStream())
            {

                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = !string.IsNullOrEmpty(folderName) ? folderName : null
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                return new ImageUploadResultModel(uploadResult.Url.ToString(), uploadResult.PublicId);
            }
        }
    }
}
