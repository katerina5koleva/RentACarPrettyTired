using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RentACar.Data.Models;
using RentACar.Repositories.Interfaces;
using RentACar.Services.Interfaces;

namespace RentACar.Services
{
    /// <summary>
    /// Provides user-related services for the Rent-A-Car system.
    /// Implements the <see cref="IUserService"/> interface.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userManager">The <see cref="UserManager{TUser}"/> instance for managing user roles and identity operations.</param>
        /// <param name="userRepository">The <see cref="IUserRepository"/> instance for accessing user data.</param>
        public UserService(UserManager<User> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Changes the role of a user based on their current role.
        /// If the user is an "Administrator", they are assigned the "BasicUser" role.
        /// If the user is a "BasicUser", they are assigned the "Administrator" role.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose role is to be changed.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a boolean indicating whether the role change was successful.
        /// </returns>
        public async Task<bool> ChangeRoleAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            // Toggle between roles
            var newRole = currentRoles.Contains("Administrator") ? "BasicUser" : "Administrator";
            await _userManager.AddToRoleAsync(user, newRole);

            return true;
        }

        /// <summary>
        /// Assigns a specific role to a user, replacing any existing roles.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to whom the role will be assigned.</param>
        /// <param name="roleName">The name of the role to assign to the user.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a boolean indicating whether the role assignment was successful.
        /// </returns>
        public async Task<bool> SetRoleAsync(string userId, string roleName)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, roleName);

            return true;
        }
    }

}
