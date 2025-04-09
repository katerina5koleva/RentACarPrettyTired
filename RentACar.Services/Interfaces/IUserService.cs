using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for user-related operations in the RentACar application.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Changes the role of a user based on their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user whose role is to be changed.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the role change was successful.</returns>
        Task<bool> ChangeRoleAsync(string userId);

        /// <summary>
        /// Assigns a specific role to a user based on their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to whom the role will be assigned.</param>
        /// <param name="roleName">The name of the role to assign to the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the role assignment was successful.</returns>
        Task<bool> SetRoleAsync(string userId, string roleName);
    }

}
