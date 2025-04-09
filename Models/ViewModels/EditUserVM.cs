using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Data.Models;

namespace RentACar.Data.ViewModels
{
    /// <summary>
    /// Represents the ViewModel for editing a user in the RentACar application.
    /// </summary>
    public class EditUserVM
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        [Required(ErrorMessage = "First name is required")]
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets the surname of the user.
        /// </summary>
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the National Identification Number (NIN) of the user.
        /// </summary>
        [Required(ErrorMessage = "National Identification Number is required.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "NIN must be 10 characters.")]
        public string NIN { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 characters.")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the role of the user in the system.
        /// </summary>
        public string Role { get; set; }
    }
}
