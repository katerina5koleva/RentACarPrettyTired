using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RentACar.Services.Interfaces;
using RentACar.Services.Helpers;

namespace RentACar.Services
{
    /// <summary>
    /// Provides functionality for managing photos using the Cloudinary service.
    /// </summary>
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoService"/> class.
        /// </summary>
        /// <param name="config">The configuration settings for Cloudinary, provided via <see cref="IOptions{TOptions}"/>.</param>
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            Account account = new Account
                (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(account);
        }

        /// <summary>
        /// Asynchronously uploads a photo to the Cloudinary service.
        /// </summary>
        /// <param name="file">The photo file to be uploaded, represented as an <see cref="IFormFile"/>.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an <see cref="ImageUploadResult"/>
        /// object with details about the uploaded photo, such as its URL and public ID.
        /// </returns>
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            ImageUploadResult uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation()
                                                .Height(500)
                                                .Width(500)
                                                .Crop("fill")
                                                .Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        /// <summary>
        /// Asynchronously deletes a photo from the Cloudinary service.
        /// </summary>
        /// <param name="publicId">The public ID of the photo to be deleted, as assigned by the Cloudinary service.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a <see cref="DeletionResult"/>
        /// object with details about the deletion operation, such as its status.
        /// </returns>
        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            DeletionParams deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}