// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIPAddressChecker.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The IPAddressChecker interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering
{
    #region Usings

    using System.Net;

    #endregion

    /// <summary>
    /// The IPAddressChecker interface.
    /// </summary>
    public interface IIPAddressChecker
    {
        #region Public Methods

        /// <summary>
        /// Checks if IP address is allowed.
        /// </summary>
        /// <param name="ipAddress">
        /// The IP address.
        /// </param>
        /// <param name="options">
        /// The IP filtering options.
        /// </param>
        /// <returns>
        /// Returns <see langword="true"/> if IP address is allowed, otherwise <see langword="false"/>.
        /// </returns>
        bool IsAllowed(IPAddress ipAddress, IPFilteringOptions options);

        #endregion
    }
}