using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentACar.Data.Models;

namespace RentACar.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for user repository operations in the Rent-A-Car system.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Checks if any user satisfies the specified condition.
        /// </summary>
        /// <param name="predicate">The condition to evaluate.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether any user satisfies the condition.</returns>
        Task<bool> AnyAsync(Expression<Func<User, bool>> predicate);

        /// <summary>
        /// Retrieves all users from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of users.</returns>
        Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user with the specified identifier, or null if not found.</returns>
        Task<User> GetByIdAsync(string id);

        /// <summary>
        /// Adds a new user to the repository with the specified password.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <param name="password">The password for the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the operation was successful.</returns>
        Task<bool> AddAsync(User user, string password);

        /// <summary>
        /// Updates an existing user in the repository.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the operation was successful.</returns>
        Task<bool> UpdateAsync(User user);

        /// <summary>
        /// Deletes a user from the repository.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the operation was successful.</returns>
        Task<bool> DeleteAsync(User user);

        /// <summary>
        /// Saves all changes made in the repository to the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the save operation was successful.</returns>
        Task<bool> SaveAsync();
    }
}
