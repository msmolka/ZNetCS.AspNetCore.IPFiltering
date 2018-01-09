// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPAddressChecker.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The IP address checker.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering.Internal
{
    #region Usings

    using System;
    using System.Linq;
    using System.Net;

    using NetTools;

    #endregion

    /// <summary>
    /// The IP address checker.
    /// </summary>
    public class IPAddressChecker : IIPAddressChecker
    {
        #region Implemented Interfaces

        #region IIPAddressChecker

        /// <inheritdoc/>
        public virtual bool IsAllowed(IPAddress ipAddress, IPFilteringOptions options)
        {
            if (ipAddress != null)
            {
                var whitelist = options.Whitelist.Select(IPAddressRange.Parse).ToList();
                var blacklist = options.Blacklist.Select(IPAddressRange.Parse).ToList();

                switch (options.DefaultBlockLevel)
                {
                    case DefaultBlockLevel.All:
                        return whitelist.Any(r => r.Contains(ipAddress)) && !blacklist.Any(r => r.Contains(ipAddress));

                    case DefaultBlockLevel.None:
                        return !blacklist.Any(r => r.Contains(ipAddress)) || whitelist.Any(r => r.Contains(ipAddress));
                }
            }

            return options.DefaultBlockLevel == DefaultBlockLevel.None;
        }

        /// <inheritdoc/>
        public bool IsIgnored(string verb, string path, IPFilteringOptions options)
        {
            if (options.IgnoredPaths != null)
            {
                string fullCheck = $"{verb}:{path}";
                string check = $"*:{path}";

                if (options.IgnoredPaths.Any(x => Contains(fullCheck, x)) || options.IgnoredPaths.Any(x => Contains(check, x)))
                {
                    return true;
                }
            }

            return false;

            bool Contains(string source, string value)
            {
                return (source != null) && (value != null) && source.StartsWith(value, StringComparison.OrdinalIgnoreCase);
            }
        }

        #endregion

        #endregion
    }
}