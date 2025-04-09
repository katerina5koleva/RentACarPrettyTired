using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentACar.Data.Helpers;
using RentACar.Data.Models;
using static RentACar.Data.Helpers.FutureDateAttribute;

namespace RentACar.Data.ViewModels
{
    public class EditRequestVM
    {
        /// <summary>
        /// Gets or sets the unique identifier for the rental request.
        /// Required for edit operations.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the selected vehicle.
        /// This field is required and must be provided by the user.
        /// </summary>
        [Required(ErrorMessage = "Vehicle selection is required")]
        [Display(Name = "Vehicle")]
        public int AutoId { get; set; }

        /// <summary>
        /// Gets or sets the display name of the current vehicle.
        /// This field is for display purposes only and is not editable.
        /// </summary>
        [Display(Name = "Current Vehicle")]
        public string CurrentAutoDisplay { get; set; }

        /// <summary>
        /// Gets or sets the pick-up date for the rental.
        /// This field is required and must be a future date.
        /// </summary>
        [Required(ErrorMessage = "Pick-up date is required")]
        [Display(Name = "Pick-Up Date")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Pick-up date must be in the future")]
        public DateTime PickUpDate { get; set; }

        /// <summary>
        /// Gets or sets the return date for the rental.
        /// This field is required and must be after the pick-up date.
        /// </summary>
        [Required(ErrorMessage = "Return date is required")]
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        [DateAfter(nameof(PickUpDate), ErrorMessage = "Return date must be after pick-up date")]
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the rental request was created.
        /// This field is for display purposes only.
        /// </summary>
        [Display(Name = "Request Date")]
        public DateTime DateOfRequest { get; set; }

        /// <summary>
        /// Gets or sets the username of the person who created the request.
        /// This field is for display purposes only.
        /// </summary>
        [Display(Name = "Requested By")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the approval status of the request.
        /// This field is intended for administrative use only.
        /// </summary>
        [Display(Name = "Approval Status")]
        public bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets the rejection status of the request.
        /// This field is intended for administrative use only.
        /// </summary>
        [Display(Name = "Rejection Status")]
        public bool IsDeclined { get; set; }

        /// <summary>
        /// Gets the total number of rental days based on the pick-up and return dates.
        /// This is a calculated property and is read-only.
        /// </summary>
        [Display(Name = "Total Rental Days")]
        public int RentalDays => (ReturnDate - PickUpDate).Days;

        /// <summary>
        /// Gets or sets the original date of the rental request.
        /// This field is for display purposes only.
        /// </summary>
        [Display(Name = "Original Request Date")]
        public DateTime OriginalRequestDate { get; set; }
    }
}