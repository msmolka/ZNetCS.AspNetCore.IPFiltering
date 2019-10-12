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
    using System.Collections.Generic;
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
        public virtual bool CheckPaths(string verb, string path, ICollection<string> paths)
        {
            if (paths != null)
            {
                string fullCheck = $"{verb}:{path}";
                string check = $"*:{path}";

                if (paths.Any(x => Contains(fullCheck, x)) || paths.Any(x => Contains(check, x)))
                {
                    return true;
                }
            }

            return false;

            static bool Contains(string source, string value)
                => (source != null) && (value != null) && source.StartsWith(value, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <inheritdoc/>
        public virtual bool IsAllowed(IPAddress ipAddress, ICollection<string> optWhitelist, ICollection<string> optBlacklist, DefaultBlockLevel blockLevel)
        {
            if (optWhitelist == null)
            {
                throw new ArgumentNullException(nameof(optWhitelist));
            }

            if (optBlacklist == null)
            {
                throw new ArgumentNullException(nameof(optBlacklist));
            }

            if (ipAddress != null)
            {
                var whitelist = optWhitelist.Select(IPAddressRange.Parse).ToList();
                var blacklist = optBlacklist.Select(IPAddressRange.Parse).ToList();

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
}