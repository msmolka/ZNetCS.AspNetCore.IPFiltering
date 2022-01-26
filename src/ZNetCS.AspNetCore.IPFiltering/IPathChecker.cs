// -----------------------------------------------------------------------
// <copyright file="IPathChecker.cs" company="Marcin Smółka">
//   Copyright (c) Marcin Smółka. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering;

#region Usings

using System.Collections.Generic;

#endregion

/// <summary>
/// Path Checker interface.
/// </summary>
public interface IPathChecker
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
    bool CheckPaths(string verb, string? path, ICollection<string>? paths);

    #endregion
}