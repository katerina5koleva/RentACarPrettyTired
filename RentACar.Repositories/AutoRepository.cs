using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Repositories.Interfaces;

namespace RentACar.Repositories
{
    /// <summary>
    /// Repository class for managing Auto entities in the Rent-A-Car system.
    /// Provides methods for CRUD operations and querying available cars.
    /// </summary>
    public class AutoRepository : IAutoRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoRepository"/> class.
        /// </summary>
        /// <param name="context">The database context for the Rent-A-Car system.</param>
        public AutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new car to the database.
        /// </summary>
        /// <param name="auto">The car to add.</param>
        /// <returns>True if the car was successfully added; otherwise, false.</returns>
        public async Task<bool> AddAsync(Auto auto)
        {
            _context.Add(auto);
            return await SaveAsync();
        }

        /// <summary>
        /// Deletes a car from the database.
        /// </summary>
        /// <param name="auto">The car to delete.</param>
        /// <returns>True if the car was successfully deleted; otherwise, false.</returns>
        public async Task<bool> DeleteAsync(Auto auto)
        {
            _context.Remove(auto);
            return await SaveAsync();
        }

        /// <summary>
        /// Retrieves all cars from the database.
        /// </summary>
        /// <returns>A collection of all cars.</returns>
        public async Task<IEnumerable<Auto>> GetAllAutosAsync()
        {
            return await _context.Autos.ToListAsync();
        }

        /// <summary>
        /// Retrieves all cars that are available for rent within a specified date range.
        /// </summary>
        /// <param name="startDate">The start date of the rental period.</param>
        /// <param name="endDate">The end date of the rental period.</param>
        /// <returns>A collection of cars available for the specified date range.</returns>
        /// <exception cref="ArgumentException">Thrown if the end date is before the start date.</exception>
        public async Task<IEnumerable<Auto>> GetAllAutosFreeAsync(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                throw new ArgumentException("End date must be after start date");

            return await _context.Autos
                .Where(a => !a.Bookings.Any(b =>
                    b.StartDate < endDate &&
                    b.EndDate > startDate))
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a car by its unique identifier.
        /// </summary>
        /// <param name="autoId">The unique identifier of the car.</param>
        /// <returns>The car with the specified identifier, or null if not found.</returns>
        public async Task<Auto> GetAutoByIdAsync(int autoId)
        {
            return await _context.Autos
                .FirstOrDefaultAsync(a => a.Id == autoId);
        }

        /// <summary>
        /// Saves changes made to the database context.
        /// </summary>
        /// <returns>True if the changes were successfully saved; otherwise, false.</returns>
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        /// <summary>
        /// Updates the details of an existing car in the database.
        /// </summary>
        /// <param name="auto">The car with updated details.</param>
        /// <returns>True if the car was successfully updated; otherwise, false.</returns>
        public async Task<bool> UpdateAsync(Auto auto)
        {
            _context.Update(auto);
            return await SaveAsync();
        }
    }


}
