using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Data.Models;

namespace RentACar.Repositories.Helpers
{
    /// <summary>
    /// Represents the result of a booking operation in the Rent-A-Car system.
    /// </summary>
    public class BookingResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the booking operation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets a message providing additional information about the booking result.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the booking period associated with the booking operation.
        /// </summary>
        public BookingPeriod Booking { get; set; }
    }
}
