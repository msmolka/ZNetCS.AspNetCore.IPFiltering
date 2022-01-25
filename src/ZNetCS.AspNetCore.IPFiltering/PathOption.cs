// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PathOption.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The path option.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering;

#region Usings

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#endregion

/// <summary>
/// The path option.
/// </summary>
public class PathOption : OptionBase
{
    #region Public Properties

    /// <summary>
    /// Gets or sets the paths specific for current configuration.
    /// </summary>
    [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Options serialization.")]
    public ICollection<string>? Paths { get; set; } = new HashSet<string>();

    #endregion
}