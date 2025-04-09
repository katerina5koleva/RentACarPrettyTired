using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Data.Models;

/// <summary>
/// Represents a user in the Rent-A-Car system.
/// Inherits from IdentityUser to include built-in identity properties.
/// </summary>
public class User : IdentityUser
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    [Required]
    public string Firstname { get; set; }

    /// <summary>
    /// Gets or sets the surname (last name) of the user.
    /// </summary>
    [Required]
    public string Surname { get; set; }

    /// <summary>
    /// Gets or sets the National Identification Number (NIN) of the user.
    /// </summary>
    [Required]
    public string NIN { get; set; }

    /// <summary>
    /// Gets or sets the username of the user.
    /// Overrides the UserName property from IdentityUser.
    /// </summary>
    [Required]
    public override string UserName { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the user.
    /// Overrides the PhoneNumber property from IdentityUser.
    /// </summary>
    [Required]
    public override string PhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// Overrides the Email property from IdentityUser.
    /// </summary>
    [Required]
    public override string Email { get; set; }

    /// <summary>
    /// Gets or sets the list of rental requests made by the user.
    /// </summary>
    public virtual List<Request> Requests { get; set; } = new List<Request>();
}
