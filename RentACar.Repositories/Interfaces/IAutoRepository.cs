using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Data.Models;

namespace RentACar.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for interacting with the Auto entity in the Rent-A-Car system.
    /// </summary>
    public interface IAutoRepository
    {
        /// <summary>
        /// Retrieves all cars available in the system.
        /// </summary>
        /// <returns>A collection of all cars.</returns>
        Task<IEnumerable<Auto>> GetAllAutosAsync();

        /// <summary>
        /// Retrieves all cars that are available for rent within a specified date range.
        /// </summary>
        /// <param name="pickUpDate">The start date of the rental period.</param>
        /// <param name="returnDate">The end date of the rental period.</param>
        /// <returns>A collection of cars available for the specified date range.</returns>
        Task<IEnumerable<Auto>> GetAllAutosFreeAsync(DateTime pickUpDate, DateTime returnDate);

        /// <summary>
        /// Retrieves a car by its unique identifier.
        /// </summary>
        /// <param name="autoId">The unique identifier of the car.</param>
        /// <returns>The car with the specified identifier, or null if not found.</returns>
        Task<Auto> GetAutoByIdAsync(int autoId);

        /// <summary>
        /// Adds a new car to the system.
        /// </summary>
        /// <param name="auto">The car to add.</param>
        /// <returns>True if the car was successfully added; otherwise, false.</returns>
        Task<bool> AddAsync(Auto auto);

        /// <summary>
        /// Updates the details of an existing car in the system.
        /// </summary>
        /// <param name="auto">The car with updated details.</param>
        /// <returns>True if the car was successfully updated; otherwise, false.</returns>
        Task<bool> UpdateAsync(Auto auto);

        /// <summary>
        /// Deletes a car from the system.
        /// </summary>
        /// <param name="auto">The car to delete.</param>
        /// <returns>True if the car was successfully deleted; otherwise, false.</returns>
        Task<bool> DeleteAsync(Auto auto);

        /// <summary>
        /// Saves changes made to the repository.
        /// </summary>
        /// <returns>True if the changes were successfully saved; otherwise, false.</returns>
        Task<bool> SaveAsync();
    }
}
