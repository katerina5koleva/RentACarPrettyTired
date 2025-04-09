using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RentACar.Data.ViewModels
{
    public class EditAutoVM
    {
        /// <summary>
        /// Gets or sets the unique identifier of the automobile.
        /// This is required for edit operations.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the brand of the automobile.
        /// This field is required and cannot exceed 50 characters.
        /// </summary>
        [Required(ErrorMessage = "Brand is required")]
        [StringLength(50, ErrorMessage = "Brand cannot exceed 50 characters")]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the model of the automobile.
        /// This field is required and cannot exceed 50 characters.
        /// </summary>
        [Required(ErrorMessage = "Model is required")]
        [StringLength(50, ErrorMessage = "Model cannot exceed 50 characters")]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the manufacturing year of the automobile.
        /// This field is required and must be between 1900 and 2100.
        /// </summary>
        [Required(ErrorMessage = "Year is required")]
        [Range(1900, 2100, ErrorMessage = "Year must be between 1900-2100")]
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the number of passenger seats in the automobile.
        /// This field is required and must be between 1 and 12.
        /// </summary>
        [Required(ErrorMessage = "Passenger seats are required")]
        [Range(1, 12, ErrorMessage = "Seats must be 1-12")]
        public int PassengerSeats { get; set; }

        /// <summary>
        /// Gets or sets the description of the automobile.
        /// This field is optional but cannot exceed 500 characters.
        /// </summary>
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the price per day for renting the automobile.
        /// This field is required and must be between $1 and $10,000.
        /// </summary>
        [Required(ErrorMessage = "Price per day is required")]
        [Range(1, 10000, ErrorMessage = "Price must be $1-$10,000")]
        public int PricePerDay { get; set; }

        /// <summary>
        /// Gets or sets the image file for the automobile.
        /// This field is used to upload a new image.
        /// </summary>
        public IFormFile Image { get; set; }

        /// <summary>
        /// Gets or sets the URL of the automobile's image.
        /// This field is optional and can be used to display the existing image.
        /// </summary>
        public string? URL { get; set; }
    }
}
