using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RentACar.Data.Models;

namespace RentACar.Services
{
    /// <summary>
    /// Provides functionality to initialize an administrator user and role in the system.
    /// </summary>
    public static class InitializeAdmin
    {
        /// <summary>
        /// Ensures that an administrator role and user exist in the system. If the administrator user
        /// already exists, their details and password are updated. If not, a new administrator user is created.
        /// </summary>
        /// <param name="serviceProvider">The service provider used to resolve dependencies such as UserManager and RoleManager.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">
        /// Thrown if there are errors during the creation, update, or password reset of the administrator user.
        /// </exception>
        public static async Task InitializeAdminAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Ensure the "Administrator" role exists
            if (!await roleManager.RoleExistsAsync("Administrator"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
            }

            // Define the administrator's email
            var adminEmail = "admin@admin.com";

            // Check if an administrator user already exists
            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin != null)
            {
                // Update the existing administrator's details
                existingAdmin.Firstname = "Admin";
                existingAdmin.Surname = "User";
                existingAdmin.NIN = "ADMIN123456";
                existingAdmin.PhoneNumber = "+1234567890";
                existingAdmin.EmailConfirmed = true;
                existingAdmin.PhoneNumberConfirmed = true;

                // Reset the administrator's password
                var resetToken = await userManager.GeneratePasswordResetTokenAsync(existingAdmin);
                var passwordResult = await userManager.ResetPasswordAsync(existingAdmin, resetToken, "SecurePassword123!");

                if (!passwordResult.Succeeded)
                {
                    throw new Exception($"Password reset failed: {string.Join(", ", passwordResult.Errors)}");
                }

                // Save the updated administrator details
                var updateResult = await userManager.UpdateAsync(existingAdmin);
                if (!updateResult.Succeeded)
                {
                    throw new Exception($"Admin update failed: {string.Join(", ", updateResult.Errors)}");
                }
            }
            else
            {
                // Create a new administrator user
                var adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Firstname = "Admin",
                    Surname = "User",
                    NIN = "ADMIN123456",
                    PhoneNumber = "+1234567890",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var createResult = await userManager.CreateAsync(adminUser, "SecurePassword123!");
                if (!createResult.Succeeded)
                {
                    throw new Exception($"Admin creation failed: {string.Join(", ", createResult.Errors)}");
                }

                // Assign the "Administrator" role to the new user
                await userManager.AddToRoleAsync(adminUser, "Administrator");
            }

            // Ensure the administrator user has the "Administrator" role
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (!await userManager.IsInRoleAsync(admin, "Administrator"))
            {
                await userManager.AddToRoleAsync(admin, "Administrator");
            }
        }
    }
}
