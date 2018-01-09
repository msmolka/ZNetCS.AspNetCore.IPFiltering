// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPFilteringOptions.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The IP filtering options.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering
{
    #region Usings

    using System.Collections.Generic;
    using System.Net;

    #endregion

    /// <summary>
    /// The IP filtering options.
    /// </summary>
    public class IPFilteringOptions
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the blacklist.
        /// </summary>
        public ICollection<string> Blacklist { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the default block level.
        /// </summary>
        public DefaultBlockLevel DefaultBlockLevel { get; set; } = DefaultBlockLevel.All;

        /// <summary>
        /// Gets or sets the http status code.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.NotFound;

        /// <summary>
        /// Gets or sets the whitelist.
        /// </summary>
        public ICollection<string> Whitelist { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the paths to be ignored for compression.
        /// </summary>
        public ICollection<string> IgnoredPaths { get; set; } = new List<string>();

        #endregion
    }
}