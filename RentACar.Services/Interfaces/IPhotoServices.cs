using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace RentACar.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for photo management services, including adding and deleting photos.
    /// </summary>
    public interface IPhotoService
    {
        /// <summary>
        /// Asynchronously uploads a photo to a cloud storage service.
        /// </summary>
        /// <param name="file">The photo file to be uploaded, represented as an <see cref="IFormFile"/>.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an <see cref="ImageUploadResult"/>
        /// object with details about the uploaded photo, such as its URL and public ID.
        /// </returns>
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        /// <summary>
        /// Asynchronously deletes a photo from the cloud storage service.
        /// </summary>
        /// <param name="publicId">The public ID of the photo to be deleted, as assigned by the cloud storage service.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a <see cref="DeletionResult"/>
        /// object with details about the deletion operation, such as its status.
        /// </returns>
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
