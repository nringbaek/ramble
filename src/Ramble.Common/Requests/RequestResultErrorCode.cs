using System;
using System.Collections.Generic;
using System.Text;

namespace Ramble
{
    public enum RequestResultErrorCode
    {
        /// <summary>
        /// No error code was specified
        /// </summary>
        None = 0,

        /// <summary>
        /// The request was unauthorized/forbidden, eg. the user is missing required roles
        /// </summary>
        Forbidden = 1,

        /// <summary>
        /// The request was invalid or is missing required information
        /// </summary>
        BadRequest = 2,

        /// <summary>
        /// If the requested resource does not exist
        /// </summary>
        NotFound = 3,

        /// <summary>
        /// Indicates that the request was invalid, eg the model is missing data
        /// </summary>
        InvalidRequest = 4
    }
}
