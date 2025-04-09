using System.Runtime.Intrinsics.Arm;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Repositories.Helpers;
using RentACar.Repositories.Interfaces;

namespace RentACar.Repositories
{
    /// <summary>
    /// Repository for managing booking periods in the Rent-A-Car system.
    /// Provides methods for booking cars, checking availability, and retrieving conflicting bookings.
    /// </summary>
    public class BookingPeriodRepository : IBookingPeriodRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingPeriodRepository"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing booking periods.</param>
        public BookingPeriodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Books a car for a specified period if it is available.
        /// </summary>
        /// <param name="autoId">The ID of the car to be booked.</param>
        /// <param name="startDate">The start date of the booking period.</param>
        /// <param name="endDate">The end date of the booking period.</param>
        /// <returns>A <see cref="BookingResult"/> indicating the success or failure of the booking.</returns>
        public async Task<BookingResult> BookAutoAsync(int autoId, DateTime startDate, DateTime endDate)
        {
            bool isAvailable = await IsAutoAvailableAsync(autoId, startDate, endDate);

            if (!isAvailable)
            {
                return new BookingResult
                {
                    Success = false,
                    Message = "Auto is not available for the selected dates"
                };
            }

            BookingPeriod booking = new BookingPeriod
            {
                AutoId = autoId,
                StartDate = startDate,
                EndDate = endDate
            };

            _context.BookingPeriods.Add(booking);
            await SaveAsync();

            return new BookingResult { Success = true, Booking = booking };
        }

        /// <summary>
        /// Checks if a car is available for a specified period.
        /// </summary>
        /// <param name="autoId">The ID of the car to check availability for.</param>
        /// <param name="startDate">The start date of the desired booking period.</param>
        /// <param name="endDate">The end date of the desired booking period.</param>
        /// <returns>A boolean indicating whether the car is available.</returns>
        /// <exception cref="ArgumentException">Thrown if the end date is not after the start date.</exception>
        public async Task<bool> IsAutoAvailableAsync(int autoId, DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                throw new ArgumentException("End date must be after start date");
            }

            return !await _context.BookingPeriods
                .AnyAsync(b => b.AutoId == autoId &&
                               b.StartDate < endDate &&
                               b.EndDate > startDate);
        }

        /// <summary>
        /// Retrieves a list of bookings that conflict with a specified period for a car.
        /// </summary>
        /// <param name="autoId">The ID of the car to check for conflicts.</param>
        /// <param name="pickUpDate">The start date of the desired booking period.</param>
        /// <param name="returnDate">The end date of the desired booking period.</param>
        /// <returns>A list of <see cref="BookingPeriod"/> objects that conflict with the specified period.</returns>
        public async Task<List<BookingPeriod>> GetConflictingBookingsAsync(int autoId, DateTime pickUpDate, DateTime returnDate)
        {
            return await _context.BookingPeriods
                .Where(bp => bp.AutoId == autoId &&
                             pickUpDate <= bp.EndDate &&
                             returnDate >= bp.StartDate)
                .ToListAsync();
        }

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        /// <returns>A boolean indicating whether the save operation was successful.</returns>
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
