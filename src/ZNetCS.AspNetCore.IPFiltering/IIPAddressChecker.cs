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

    using System;
    using System.Collections.Generic;
    using System.Net;

    using NetTools;

    #endregion

    /// <summary>
    /// The IPAddressChecker interface.
    /// </summary>
    public interface IIPAddressChecker
    {
        #region Public Methods

        /// <summary>
        /// Check if path is on given list.
        /// </summary>
        /// <param name="verb">
        /// The HTTP verb.
        /// </param>
        /// <param name="path">
        /// The request path.
        /// </param>
        /// <param name="paths">
        /// The paths to check.
        /// </param>
        /// <returns>
        /// Returns <see langword="true"/> if request path is on give list, otherwise <see langword="false"/>.
        /// </returns>
        bool CheckPaths(string verb, string path, ICollection<string> paths);

        /// <summary>
        /// Checks if IP address is allowed.
        /// </summary>
        /// <param name="ipAddress">
        /// The IP address.
        /// </param>
        /// <param name="whitelist">
        /// The whitelist.
        /// </param>
        /// <param name="blacklist">
        /// The blacklist.
        /// </param>
        /// <param name="blockLevel">
        /// The default block level.
        /// </param>
        /// <returns>
        /// Returns <see langword="true"/> if IP address is allowed, otherwise <see langword="false"/>.
        /// </returns>
        bool IsAllowed(IPAddress ipAddress, ICollection<IPAddressRange> whitelist, ICollection<IPAddressRange> blacklist, DefaultBlockLevel blockLevel);

        #endregion
    }
}