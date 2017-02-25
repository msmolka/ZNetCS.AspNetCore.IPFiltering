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
    public enum DefaultBlockLevel : byte
    {
        /// <summary>
        /// The none.
        /// </summary>
        None = 0,

        /// <summary>
        /// The all.
        /// </summary>
        All = 1
    }
}