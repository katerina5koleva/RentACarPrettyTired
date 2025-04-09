using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Data.Models
{
    /// <summary>
    /// Represents a booking period for a car in the Rent-A-Car system.
    /// </summary>
    public class BookingPeriod
    {
        /// <summary>
        /// Gets or sets the unique identifier for the booking period.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the start date of the booking period.
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the booking period.
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated car (Auto).
        /// </summary>
        [ForeignKey("AutoId")]
        public int AutoId { get; set; }

        /// <summary>
        /// Gets or sets the car (Auto) associated with this booking period.
        /// </summary>
        public Auto Auto { get; set; }
    }
    
}

