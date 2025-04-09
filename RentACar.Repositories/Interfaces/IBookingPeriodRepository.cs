using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Data.Models;
using RentACar.Repositories.Helpers;

namespace RentACar.Repositories.Interfaces
{
    /// <summary>
    /// Interface for managing booking periods in the Rent-A-Car system.
    /// Provides methods for checking availability, retrieving conflicting bookings, and creating new bookings.
    /// </summary>
    public interface IBookingPeriodRepository
    {
        /// <summary>
        /// Retrieves a list of booking periods that conflict with the specified date range for a given car.
        /// </summary>
        /// <param name="autoId">The unique identifier of the car.</param>
        /// <param name="pickUpDate">The start date of the booking period.</param>
        /// <param name="returnDate">The end date of the booking period.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of conflicting booking periods.</returns>
        Task<List<BookingPeriod>> GetConflictingBookingsAsync(int autoId, DateTime pickUpDate, DateTime returnDate);

        /// <summary>
        /// Checks if a car is available for booking within the specified date range.
        /// </summary>
        /// <param name="autoId">The unique identifier of the car.</param>
        /// <param name="startDate">The start date of the desired booking period.</param>
        /// <param name="endDate">The end date of the desired booking period.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the car is available; otherwise, false.</returns>
        Task<bool> IsAutoAvailableAsync(int autoId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Creates a new booking for a car within the specified date range.
        /// </summary>
        /// <param name="autoId">The unique identifier of the car.</param>
        /// <param name="startDate">The start date of the booking period.</param>
        /// <param name="endDate">The end date of the booking period.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the booking operation, including success status and any relevant messages.</returns>
        Task<BookingResult> BookAutoAsync(int autoId, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Saves any changes made to the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result is true if the changes were successfully saved; otherwise, false.</returns>
        Task<bool> SaveAsync();
    }
}
