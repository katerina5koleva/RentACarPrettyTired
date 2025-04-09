using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Services.Helpers
{
    /// <summary>
    /// Represents the configuration settings required to integrate with the Cloudinary service.
    /// </summary>
    public class CloudinarySettings
    {
        /// <summary>
        /// Gets or sets the name of the Cloudinary cloud account.
        /// This is used to identify the specific Cloudinary account to interact with.
        /// </summary>
        public string CloudName { get; set; }

        /// <summary>
        /// Gets or sets the API key for authenticating requests to the Cloudinary service.
        /// This key is provided by Cloudinary and should be kept secure.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the API secret for authenticating requests to the Cloudinary service.
        /// This secret is provided by Cloudinary and should be kept confidential.
        /// </summary>
        public string ApiSecret { get; set; }
    }
}