using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Data.Models;

namespace RentACar.Repositories.Interfaces
{
    /// <summary>
    /// Interface for managing rental requests in the Rent-A-Car system.
    /// Provides methods for CRUD operations and request-specific actions.
    /// </summary>
    public interface IRequestRepository
    {
        /// <summary>
        /// Retrieves all rental requests asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of all requests.</returns>
        Task<IEnumerable<Request>> GetAllRequestsAsync();

        /// <summary>
        /// Retrieves all rental requests that have been answered (approved or declined).
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of answered requests.</returns>
        Task<IEnumerable<Request>> GetAllRequestsAnswered();

        /// <summary>
        /// Retrieves all rental requests that have not been answered yet.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of unanswered requests.</returns>
        Task<IEnumerable<Request>> GetAllRequestsUnanswered();

        /// <summary>
        /// Retrieves all rental requests made by a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a collection of requests made by the user.</returns>
        Task<IEnumerable<Request>> GetAllByUserId(string userId);

        /// <summary>
        /// Retrieves a specific rental request by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the request.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the request if found, otherwise null.</returns>
        Task<Request> GetRequestByIdAsync(int id);

        /// <summary>
        /// Approves a rental request by its unique identifier.
        /// </summary>
        /// <param name="requestId">The unique identifier of the request to approve.</param>
        /// <returns>A task representing the asynchronous operation. The task result indicates whether the operation was successful.</returns>
        Task<bool> ApproveRequest(int requestId);

        /// <summary>
        /// Declines a rental request by its unique identifier.
        /// </summary>
        /// <param name="requestId">The unique identifier of the request to decline.</param>
        /// <returns>A task representing the asynchronous operation. The task result indicates whether the operation was successful.</returns>
        Task<bool> DeclineRequest(int requestId);

        /// <summary>
        /// Adds a new rental request to the system.
        /// </summary>
        /// <param name="request">The rental request to add.</param>
        /// <returns>A task representing the asynchronous operation. The task result indicates whether the operation was successful.</returns>
        Task<bool> AddAsync(Request request);

        /// <summary>
        /// Updates an existing rental request in the system.
        /// </summary>
        /// <param name="request">The rental request to update.</param>
        /// <returns>A task representing the asynchronous operation. The task result indicates whether the operation was successful.</returns>
        Task<bool> UpdateAsync(Request request);

        /// <summary>
        /// Deletes a rental request from the system.
        /// </summary>
        /// <param name="request">The rental request to delete.</param>
        /// <returns>A task representing the asynchronous operation. The task result indicates whether the operation was successful.</returns>
        Task<bool> DeleteAsync(Request request);

        /// <summary>
        /// Saves changes made to the repository.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result indicates whether the operation was successful.</returns>
        Task<bool> SaveAsync();
    }
}
