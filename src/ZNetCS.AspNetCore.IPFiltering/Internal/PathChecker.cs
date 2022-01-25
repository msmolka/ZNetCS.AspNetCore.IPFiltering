// -----------------------------------------------------------------------
// <copyright file="PathChecker.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering.Internal;

#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

#endregion

/// <summary>
/// Path Checker implementation.
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global", Justification = "Public API")]
public class PathChecker : IPathChecker
{
    #region Implemented Interfaces

    #region IPathChecker

    /// <inheritdoc/>
    public virtual bool CheckPaths(string verb, string? path, ICollection<string>? paths)
    {
        if (paths == null)
        {
            return false;
        }

        string fullCheck = $"{verb}:{path}";
        string check = $"*:{path}";

        return paths.Any(x => Contains(fullCheck, x)) || paths.Any(x => Contains(check, x));

        static bool Contains(string source, string value) => source.StartsWith(value, StringComparison.InvariantCultureIgnoreCase);
    }

    #endregion

    #endregion
}