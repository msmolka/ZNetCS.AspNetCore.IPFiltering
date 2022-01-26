// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionBase.cs" company="Marcin Smółka">
//   Copyright (c) Marcin Smółka. All rights reserved.
// </copyright>
// <summary>
//   The option base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering;

#region Usings

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;

using NetTools;

#endregion

/// <summary>
/// The option base.
/// </summary>
public abstract class OptionBase
{
    #region Fields

    /// <summary>
    /// The blacklist.
    /// </summary>
    private ICollection<string> blacklist = new HashSet<string>();

    /// <summary>
    /// The whitelist.
    /// </summary>
    private ICollection<string> whitelist = new HashSet<string>();

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the blacklist.
    /// </summary>
    [AllowNull]
    [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Options serialization.")]
    public ICollection<string> Blacklist
    {
        get => this.blacklist;
        set
        {
            this.blacklist = value ?? new HashSet<string>();
            this.ParsedBlacklist = this.blacklist.Select(IPAddressRange.Parse).ToList();
        }
    }

    /// <summary>
    /// Gets or sets the default block level.
    /// </summary>
    public DefaultBlockLevel DefaultBlockLevel { get; set; } = DefaultBlockLevel.All;

    /// <summary>
    /// Gets or sets the http status code.
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.NotFound;

    /// <summary>
    /// Gets the parsed blacklist.
    /// </summary>
    public ICollection<IPAddressRange> ParsedBlacklist { get; private set; } = new HashSet<IPAddressRange>();

    /// <summary>
    /// Gets the parsed whitelist.
    /// </summary>
    public ICollection<IPAddressRange> ParsedWhitelist { get; private set; } = new HashSet<IPAddressRange>();

    /// <summary>
    /// Gets or sets the whitelist.
    /// </summary>
    [AllowNull]
    [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Options serialization.")]
    public ICollection<string> Whitelist
    {
        get => this.whitelist;
        set
        {
            this.whitelist = value ?? new HashSet<string>();
            this.ParsedWhitelist = this.whitelist.Select(IPAddressRange.Parse).ToList();
        }
    }

    #endregion
}