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
    /// Represents a ViewModel for user data in the Rent-A-Car system.
    /// This class is used to transfer user-related data between the UI and the application logic.
    /// </summary>
    public class UserVM
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// This field is required.
        /// </summary>
        [Required(ErrorMessage = "First name is required.")]
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets the surname (last name) of the user.
        /// This field is required.
        /// </summary>
        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the National Identification Number (NIN) of the user.
        /// This field is required and must be between 5 and 20 characters.
        /// </summary>
        [Required(ErrorMessage = "National Identification Number is required.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "NIN must be between 5 and 20 characters.")]
        public string NIN { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// This field is required and must be in a valid phone number format.
        /// </summary>
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// This field is required and must be in a valid email address format.
        /// </summary>
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the role of the user in the system (e.g., Admin, Customer).
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the list of rental requests made by the user.
        /// This property is optional and can be null.
        /// </summary>
        public virtual List<Request>? Requests { get; set; }
    }
}
