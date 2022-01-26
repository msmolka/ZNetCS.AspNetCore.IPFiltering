// -----------------------------------------------------------------------
// <copyright file="LoggerExtensions.cs" company="Marcin Smółka">
//   Copyright (c) Marcin Smółka. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering.Internal;

#region Usings

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

using Microsoft.Extensions.Logging;

#endregion

[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Internal")]
internal static class LoggerExtensions
{
    #region Static Fields

    private static readonly Action<ILogger, IPAddress?, Exception?> AllowedIPAddressDefinition =
        LoggerMessage.Define<IPAddress?>(LogLevel.Debug, new EventId(105, "FoundIPAddress"), "The IP: {IPAddress} is allowed for further process");

    private static readonly Action<ILogger, IPAddress?, Exception?> BlockedIPAddressDefinition =
        LoggerMessage.Define<IPAddress?>(LogLevel.Information, new EventId(1, "BlockedIPAddress"), "The IP Address: {IPAddress} was blocked");

    private static readonly Action<ILogger, IPAddress?, Exception?> CheckIPAddressDefinition =
        LoggerMessage.Define<IPAddress?>(LogLevel.Debug, new EventId(104, "CheckIPAddress"), "Checking if IP: {IPAddress} address is allowed");

    private static readonly Action<ILogger, string?, string, Exception?> CheckPathDefinition1 =
        LoggerMessage.Define<string?, string>(
            LogLevel.Debug,
            new EventId(102, "CheckPath"),
            "Checking if path: {Path} with method {Verb} is on any specific path option");

    private static readonly Action<ILogger, string?, string, IPAddress?, Exception?> CheckPathDefinition2 =
        LoggerMessage.Define<string?, string, IPAddress?>(
            LogLevel.Debug,
            new EventId(103, "CheckPath"),
            "Checking if path: {Path} with method {Verb} should be ignored or IP: {IPAddress} address is allowed");

    private static readonly Action<ILogger, IPAddress?, Exception?> FoundIPAddressDefinition1 =
        LoggerMessage.Define<IPAddress?>(LogLevel.Debug, new EventId(101, "FoundIPAddress"), "Found IP: {IPAddress}");

    private static readonly Action<ILogger, IPAddress?, string, Exception?> FoundIPAddressDefinition2 =
        LoggerMessage.Define<IPAddress?, string>(LogLevel.Debug, new EventId(100, "FoundIPAddress"), "Found IP: {IPAddress} in {Header}");

    #endregion

    #region Public Methods

    public static void AllowedIPAddress(this ILogger logger, IPAddress? ipAddress)
    {
        AllowedIPAddressDefinition(logger, ipAddress, null);
    }

    public static void BlockedIPAddress(this ILogger logger, IPAddress? ipAddress)
    {
        BlockedIPAddressDefinition(logger, ipAddress, null);
    }

    public static void CheckIPAddress(this ILogger logger, IPAddress? ipAddress)
    {
        CheckIPAddressDefinition(logger, ipAddress, null);
    }

    public static void CheckPath(this ILogger logger, string? path, string verb)
    {
        CheckPathDefinition1(logger, path, verb, null);
    }

    public static void CheckPath(this ILogger logger, string? path, string verb, IPAddress? ipAddress)
    {
        CheckPathDefinition2(logger, path, verb, ipAddress, null);
    }

    public static void FoundIPAddress(this ILogger logger, IPAddress? ipAddress, string header)
    {
        FoundIPAddressDefinition2(logger, ipAddress, header, null);
    }

    public static void FoundIPAddress(this ILogger logger, IPAddress? ipAddress)
    {
        FoundIPAddressDefinition1(logger, ipAddress, null);
    }

    #endregion
}