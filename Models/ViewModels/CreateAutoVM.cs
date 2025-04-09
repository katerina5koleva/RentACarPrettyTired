using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace RentACar.Data.ViewModels
{

    /// <summary>
    /// ViewModel for creating a new automobile in the Rent-A-Car system.
    /// </summary>
    public class CreateAutoVM
    {
        /// <summary>
        /// Gets or sets the brand of the automobile.
        /// </summary>
        /// <remarks>
        /// This field is required and cannot exceed 50 characters.
        /// </remarks>
        [Required(ErrorMessage = "Brand is required")]
        [StringLength(50, ErrorMessage = "Brand cannot be longer than 50 characters")]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the model of the automobile.
        /// </summary>
        /// <remarks>
        /// This field is required and cannot exceed 50 characters.
        /// </remarks>
        [Required(ErrorMessage = "Model is required")]
        [StringLength(50, ErrorMessage = "Model cannot be longer than 50 characters")]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the manufacturing year of the automobile.
        /// </summary>
        /// <remarks>
        /// This field is required and must be between 1900 and 2100.
        /// </remarks>
        [Required(ErrorMessage = "Year is required")]
        [Range(1900, 2100, ErrorMessage = "Year must be between 1900 and 2100")]
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the number of passenger seats in the automobile.
        /// </summary>
        /// <remarks>
        /// This field is required and must be between 1 and 12.
        /// </remarks>
        [Required(ErrorMessage = "Number of passenger seats is required")]
        [Range(1, 12, ErrorMessage = "Seats must be between 1 and 12")]
        public int PassengerSeats { get; set; }

        /// <summary>
        /// Gets or sets the description of the automobile.
        /// </summary>
        /// <remarks>
        /// This field is optional but cannot exceed 500 characters.
        /// </remarks>
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the price per day for renting the automobile.
        /// </summary>
        /// <remarks>
        /// This field is required and must be between 1 and 10,000.
        /// </remarks>
        [Required(ErrorMessage = "Price per day is required")]
        [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10,000")]
        public int PricePerDay { get; set; }

        /// <summary>
        /// Gets or sets the image file for the automobile.
        /// </summary>
        /// <remarks>
        /// This field is required and represents the uploaded image file.
        /// </remarks>
        [Required]
        public IFormFile Image { get; set; }
    }


}
