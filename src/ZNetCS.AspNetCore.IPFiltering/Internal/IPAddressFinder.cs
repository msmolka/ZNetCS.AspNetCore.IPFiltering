// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPAddressFinder.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The IP address finder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering.Internal;

#region Usings

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

#endregion

/// <summary>
/// The IP address finder.
/// </summary>
[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global", Justification = "Public API")]
public class IPAddressFinder : IIPAddressFinder
{
    #region Constants

    /// <summary>
    /// The X-Forwarded-For header.
    /// </summary>
    private const string ForwardedFor = "X-Forwarded-For";

    /// <summary>
    /// The X-Real-IP header.
    /// </summary>
    private const string RealIP = "X-Real-IP";

    #endregion

    #region Fields

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger logger;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="IPAddressFinder"/> class.
    /// </summary>
    /// <param name="loggerFactory">
    /// The logger factory.
    /// </param>
    public IPAddressFinder(ILoggerFactory loggerFactory) => this.logger = loggerFactory.CreateLogger<IPAddressFinder>();

    #endregion

    #region Implemented Interfaces

    #region IIPAddressFinder

    /// <inheritdoc/>
    public virtual IPAddress? Find(HttpContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        string realIpHeader = context.Request.Headers[RealIP];
        IPAddress? ipAddress = null;
        if (!string.IsNullOrWhiteSpace(realIpHeader))
        {
            ipAddress = IPAddressParser.Parse(realIpHeader);
        }

        if (ipAddress != null)
        {
            this.logger.LogDebug("Found IP: {IPAddress} in {Header}", ipAddress, RealIP);
            return ipAddress;
        }

        string[]? forwardedForHeader = context.Request.Headers.GetCommaSeparatedValues(ForwardedFor);

        if (forwardedForHeader is { Length: > 0 })
        {
            // first address in X-Forwarded-For header is original one.
            // X-Forwarded-For: client, proxy1, proxy2
            ipAddress = IPAddressParser.Parse(forwardedForHeader[0]);

            if (ipAddress != null)
            {
                this.logger.LogDebug("Found IP: {IPAddress} in {Header}", ipAddress, ForwardedFor);
                return ipAddress;
            }
        }

        ipAddress = context.Connection.RemoteIpAddress;

        this.logger.LogDebug("Found IP: {IPAddress}", ipAddress);
        return ipAddress;
    }

    #endregion

    #endregion
}