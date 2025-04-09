using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Repositories.Helpers;
using RentACar.Repositories.Interfaces;

namespace RentACar.Repositories
{
    /// <summary>
    /// Repository class for managing rental requests in the Rent-A-Car system.
    /// Provides methods for CRUD operations and specific business logic related to requests.
    /// </summary>
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookingPeriodRepository _bookingPeriodRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestRepository"/> class.
        /// </summary>
        /// <param name="context">The database context for the application.</param>
        /// <param name="bookingPeriodRepository">The repository for managing booking periods.</param>
        public RequestRepository(ApplicationDbContext context, IBookingPeriodRepository bookingPeriodRepository)
        {
            _context = context;
            _bookingPeriodRepository = bookingPeriodRepository;
        }

        /// <summary>
        /// Retrieves all rental requests from the database.
        /// </summary>
        /// <returns>A list of all rental requests, including associated Auto and User entities.</returns>
        public async Task<IEnumerable<Request>> GetAllRequestsAsync()
        {
            return await _context.Requests.Include(x => x.Auto)
                                          .Include(x => x.User)
                                          .ToListAsync();
        }

        /// <summary>
        /// Retrieves all rental requests made by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of rental requests made by the specified user, including associated Auto entities.</returns>
        public async Task<IEnumerable<Request>> GetAllByUserId(string userId)
        {
            return await _context.Requests
                .Where(x => x.UserId == userId)
                .Include(x => x.Auto)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves all rental requests that have been answered (approved or declined).
        /// </summary>
        /// <returns>A list of answered rental requests, including associated Auto and User entities.</returns>
        public async Task<IEnumerable<Request>> GetAllRequestsAnswered()
        {
            return await _context.Requests
                .Where(x => x.IsApproved == true || x.IsDeclined == true)
                .Include(x => x.Auto)
                .Include(x => x.User)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves all rental requests that have not been answered (neither approved nor declined).
        /// </summary>
        /// <returns>A list of unanswered rental requests, including associated Auto and User entities.</returns>
        public async Task<IEnumerable<Request>> GetAllRequestsUnanswered()
        {
            return await _context.Requests
                .Where(x => x.IsApproved == false && x.IsDeclined == false)
                .Include(x => x.Auto)
                .Include(x => x.User)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific rental request by its ID.
        /// </summary>
        /// <param name="id">The ID of the rental request.</param>
        /// <returns>The rental request with the specified ID, including associated Auto and User entities.</returns>
        public async Task<Request> GetRequestByIdAsync(int id)
        {
            return await _context.Requests.Where(x => x.Id == id)
                                          .Include(x => x.User)
                                          .Include(x => x.Auto)
                                          .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Approves a rental request and books the associated car for the requested period.
        /// </summary>
        /// <param name="requestId">The ID of the rental request to approve.</param>
        /// <returns>True if the request was successfully approved and the car was booked; otherwise, false.</returns>
        public async Task<bool> ApproveRequest(int requestId)
        {
            Request request = await GetRequestByIdAsync(requestId);
            BookingResult book = await _bookingPeriodRepository
                                       .BookAutoAsync(request.AutoId, request.PickUpDate, request.ReturnDate);
            if (!book.Success)
            {
                return false;
            }
            else
            {
                int updatedCount = await _context.Requests
                    .Where(r => r.Id == requestId)
                    .ExecuteUpdateAsync(r => r.SetProperty(x => x.IsApproved, true));
                return updatedCount > 0;
            }
        }

        /// <summary>
        /// Declines a rental request.
        /// </summary>
        /// <param name="requestId">The ID of the rental request to decline.</param>
        /// <returns>True if the request was successfully declined; otherwise, false.</returns>
        public async Task<bool> DeclineRequest(int requestId)
        {
            return await _context.Requests.Where(r => r.Id == requestId)
                .ExecuteUpdateAsync(r => r.SetProperty(x => x.IsDeclined, true)) > 0;
        }

        /// <summary>
        /// Adds a new rental request to the database.
        /// </summary>
        /// <param name="request">The rental request to add.</param>
        /// <returns>True if the request was successfully added; otherwise, false.</returns>
        public async Task<bool> AddAsync(Request request)
        {
            _context.Add(request);
            return await SaveAsync();
        }

        /// <summary>
        /// Deletes an existing rental request from the database.
        /// </summary>
        /// <param name="request">The rental request to delete.</param>
        /// <returns>True if the request was successfully deleted; otherwise, false.</returns>
        public async Task<bool> DeleteAsync(Request request)
        {
            _context.Remove(request);
            return await SaveAsync();
        }

        /// <summary>
        /// Updates an existing rental request in the database.
        /// </summary>
        /// <param name="request">The rental request to update.</param>
        /// <returns>True if the request was successfully updated; otherwise, false.</returns>
        public async Task<bool> UpdateAsync(Request request)
        {
            _context.Update(request);
            return await SaveAsync();
        }

        /// <summary>
        /// Saves changes made to the database context.
        /// </summary>
        /// <returns>True if changes were successfully saved; otherwise, false.</returns>
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
