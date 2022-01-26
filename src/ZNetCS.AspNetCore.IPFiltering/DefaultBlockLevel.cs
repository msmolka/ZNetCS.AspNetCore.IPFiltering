// -----------------------------------------------------------------------
// <copyright file="DefaultBlockLevel.cs" company="Marcin Smółka">
// Copyright (c) Marcin Smółka. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering;

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