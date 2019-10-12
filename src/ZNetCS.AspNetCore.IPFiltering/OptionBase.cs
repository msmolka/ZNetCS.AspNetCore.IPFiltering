// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionBase.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The option base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering
{
    #region Usings

    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;

    #endregion

    /// <summary>
    /// The option base.
    /// </summary>
    public abstract class OptionBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the blacklist.
        /// </summary>
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Options serialization.")]
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
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Options serialization.")]
        public ICollection<string> Whitelist { get; set; } = new List<string>();

        #endregion
    }
}