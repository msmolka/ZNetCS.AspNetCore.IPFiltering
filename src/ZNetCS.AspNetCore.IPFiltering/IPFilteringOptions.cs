// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPFilteringOptions.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The IP filtering options.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering;

#region Usings

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#endregion

/// <summary>
/// The IP filtering options.
/// </summary>
public class IPFilteringOptions : OptionBase
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the paths to be ignored from filtering.
    /// </summary>
    [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Options serialization.")]
    public ICollection<string>? IgnoredPaths { get; set; } = new HashSet<string>();

    /// <summary>
    /// Gets or sets the paths specific filtering.
    /// </summary>
    [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Options serialization.")]
    public ICollection<PathOption>? PathOptions { get; set; } = new HashSet<PathOption>();

    #endregion
}