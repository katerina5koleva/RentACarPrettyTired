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
    /// Represents a rental request in the Rent-A-Car system.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Gets or sets the unique identifier for the request.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the associated car (Auto).
        /// </summary>
        [ForeignKey("AutoId")]
        public int AutoId { get; set; }

        /// <summary>
        /// Gets or sets the car (Auto) associated with the request.
        /// </summary>
        public Auto Auto { get; set; }

        /// <summary>
        /// Gets or sets the date when the car is to be picked up.
        /// </summary>
        [Required]
        public DateTime PickUpDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the car is to be returned.
        /// </summary>
        [Required]
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// Gets or sets the foreign key for the user who made the request.
        /// </summary>
        [ForeignKey("UserId")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the request has been approved.
        /// </summary>
        public bool IsApproved { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the request has been declined.
        /// </summary>
        public bool IsDeclined { get; set; } = false;

        /// <summary>
        /// Gets or sets the date when the request was created.
        /// </summary>
        [Required]
        public DateTime DateOfRequest { get; set; }

        /// <summary>
        /// Gets or sets the user who made the request.
        /// </summary>
        public User User { get; set; }
    }
}