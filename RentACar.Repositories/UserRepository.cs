using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Repositories.Interfaces;

namespace RentACar.Repositories
{

    /// <summary>
    /// Repository class for managing user-related operations in the Rent-A-Car system.
    /// Provides methods for CRUD operations and user role management.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The database context for the application.</param>
        /// <param name="userManager">The user manager for handling user-related operations.</param>
        /// <param name="roleManager">The role manager for handling role-related operations.</param>
        public UserRepository(
            ApplicationDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Checks if any user satisfies the specified condition.
        /// </summary>
        /// <param name="predicate">The condition to evaluate.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether any user satisfies the condition.</returns>
        public async Task<bool> AnyAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.AnyAsync(predicate);
        }

        /// <summary>
        /// Adds a new user to the repository with the specified password.
        /// Assigns the "BasicUser" role to the user upon successful creation.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <param name="password">The password for the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the operation was successful.</returns>
        public async Task<bool> AddAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "BasicUser");
            }
            return result.Succeeded;
        }

        /// <summary>
        /// Deletes a user from the repository.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the operation was successful.</returns>
        public async Task<bool> DeleteAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        /// <summary>
        /// Retrieves all users from the repository, including their associated rental requests.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of users.</returns>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Requests)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a user by their unique identifier, including their associated rental requests.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user with the specified identifier, or null if not found.</returns>
        public async Task<User> GetByIdAsync(string id)
        {
            return await _context.Users
                .Include(u => u.Requests)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// Saves all changes made in the repository to the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the save operation was successful.</returns>
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        /// <summary>
        /// Updates an existing user in the repository.
        /// </summary>
        /// <param name="user">The user to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the operation was successful.</returns>
        public async Task<bool> UpdateAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }

}
