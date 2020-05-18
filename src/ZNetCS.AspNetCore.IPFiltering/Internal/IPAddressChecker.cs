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

        /// <inheritdoc />
        public bool IsAllowed(IPAddress ipAddress, ICollection<IPAddressRange> whitelist, ICollection<IPAddressRange> blacklist, DefaultBlockLevel blockLevel)
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
}