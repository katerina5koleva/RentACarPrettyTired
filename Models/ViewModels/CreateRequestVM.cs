using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Data.Helpers;
using RentACar.Data.Models;
using static RentACar.Data.Helpers.FutureDateAttribute;

namespace RentACar.Data.ViewModels
{
    /// <summary>
    /// ViewModel for creating a rental request in the Rent-A-Car system.
    /// </summary>
    public class CreateRequestVM
    {
        /// <summary>
        /// Gets or sets the ID of the selected vehicle.
        /// </summary>
        [Required(ErrorMessage = "Vehicle selection is required")]
        [Display(Name = "Vehicle")]
        public int AutoId { get; set; }

        /// <summary>
        /// Gets or sets the pick-up date for the rental.
        /// </summary>
        [Required(ErrorMessage = "Pick-up date is required")]
        [Display(Name = "Pick-Up Date")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Pick-up date must be in the future")]
        public DateTime PickUpDate { get; set; } = DateTime.Today.AddDays(1);

        /// <summary>
        /// Gets or sets the return date for the rental.
        /// </summary>
        [Required(ErrorMessage = "Return date is required")]
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        [DateAfter(nameof(PickUpDate), ErrorMessage = "Return date must be after pick-up date")]
        public DateTime ReturnDate { get; set; } = DateTime.Today.AddDays(2);

        /// <summary>
        /// Gets or sets the list of available vehicles for selection.
        /// </summary>
        public IEnumerable<Auto> AvailableAutos { get; set; } = new List<Auto>();
    }
}
