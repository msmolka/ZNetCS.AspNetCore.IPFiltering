// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultBlockLevel.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The default block level.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering
{
    /// <summary>
    /// The default block level.
    /// </summary>
    public enum DefaultBlockLevel
    {
        /// <summary>
        /// None of request will be blocked if not blacklisted.
        /// </summary>
        None = 0,

        /// <summary>
        /// All requests will be blocked if not whitelisted.
        /// </summary>
        All = 1
    }
}