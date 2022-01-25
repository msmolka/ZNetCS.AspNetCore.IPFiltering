// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPAddressChecker.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The IP address checker.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering.Internal;

#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;

using NetTools;

#endregion

/// <summary>
/// The IP address checker.
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global", Justification = "Public API")]
public class IPAddressChecker : IIPAddressChecker
{
    #region Implemented Interfaces

    #region IIPAddressChecker

    /// <inheritdoc />
    public virtual bool IsAllowed(IPAddress? ipAddress, ICollection<IPAddressRange> whitelist, ICollection<IPAddressRange> blacklist, DefaultBlockLevel blockLevel)
    {
        if (whitelist == null)
        {
            throw new ArgumentNullException(nameof(whitelist));
        }

        if (blacklist == null)
        {
            throw new ArgumentNullException(nameof(blacklist));
        }

        if (ipAddress != null)
        {
            switch (blockLevel)
            {
                case DefaultBlockLevel.All:
                    return whitelist.Any(r => r.Contains(ipAddress)) && !blacklist.Any(r => r.Contains(ipAddress));

                case DefaultBlockLevel.None:
                    return !blacklist.Any(r => r.Contains(ipAddress)) || whitelist.Any(r => r.Contains(ipAddress));
            }
        }

        return blockLevel == DefaultBlockLevel.None;
    }

    #endregion

    #endregion
}