// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIPAddressFinder.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The IPAddressFinder interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering;

#region Usings

using System.Net;

using Microsoft.AspNetCore.Http;

#endregion

/// <summary>
/// The IPAddressFinder interface.
/// </summary>
public interface IIPAddressFinder
{
    #region Public Methods

    /// <summary>
    /// Finds IP address in HTTP context.
    /// </summary>
    /// <param name="context">
    /// The HTTP context.
    /// </param>
    IPAddress? Find(HttpContext context);

    #endregion
}