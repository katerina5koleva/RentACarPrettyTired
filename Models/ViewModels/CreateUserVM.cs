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
    /// ViewModel for creating a new user in the Rent-A-Car system.
    /// Contains properties for user input validation and data binding.
    /// </summary>
    public class CreateUserVM
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        [Required(ErrorMessage = "First name is required.")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        [Required(ErrorMessage = "First name is required.")]
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets the surname (last name) of the user.
        /// </summary>
        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the National Identification Number (NIN) of the user.
        /// Must be exactly 10 characters long and follow a valid phone number format.
        /// </summary>
        [Required(ErrorMessage = "National Identification Number is required.")]
        [Phone(ErrorMessage = "Invalid NIN format.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "NIN must be 10 characters.")]
        public string NIN { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// Must be exactly 10 characters long and follow a valid phone number format.
        /// </summary>
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be 10 characters.")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// Must follow a valid email address format.
        /// </summary>
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password for the user.
        /// Must be between 6 and 100 characters long.
        /// </summary>
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirmation password for the user.
        /// Must match the value of the Password property.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the list of rental requests associated with the user.
        /// </summary>
        public virtual List<Request> Requests { get; set; } = new();
    }

}
