using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Data.Models
{
    /// <summary>
    /// Represents a car available for rent in the Rent-A-Car system.
    /// </summary>
    public class Auto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the car.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the brand of the car (e.g., Toyota, Ford).
        /// </summary>
        [Required]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets the model of the car (e.g., Corolla, Mustang).
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the manufacturing year of the car.
        /// </summary>
        [Required]
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the number of passenger seats in the car.
        /// </summary>
        [Required]
        public int PassengerSeats { get; set; }

        /// <summary>
        /// Gets or sets an optional description of the car.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the URL or path to the image of the car.
        /// </summary>
        [Required]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the rental price per day for the car.
        /// </summary>
        [Required]
        public int PricePerDay { get; set; }

        /// <summary>
        /// Gets or sets the list of rental requests associated with the car.
        /// </summary>
        public virtual List<Request> Requests { get; set; } = new List<Request>();

        /// <summary>
        /// Gets or sets the list of booking periods for the car.
        /// </summary>
        public virtual List<BookingPeriod> Bookings { get; set; } = new List<BookingPeriod>();
    }
}
