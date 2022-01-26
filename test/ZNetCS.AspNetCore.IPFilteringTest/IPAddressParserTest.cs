// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPAddressParserTest.cs" company="Marcin Smółka">
//   Copyright (c) Marcin Smółka. All rights reserved.
// </copyright>
// <summary>
//   The IP test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFilteringTest;

#region Usings

using System.Net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZNetCS.AspNetCore.IPFiltering.Internal;

#endregion

/// <summary>
/// The IP test.
/// </summary>
[TestClass]
public class IPAddressParserTest
{
    #region Public Methods

    /// <summary>
    /// The IP 4 port test.
    /// </summary>
    [TestMethod]
    public void ParseIpv4PortTest()
    {
        // Arrange
        IPAddress? ip = IPAddressParser.Parse("192.168.0.1:8080");

        // Assert
        Assert.AreEqual("192.168.0.1", ip?.ToString(), "Equal");
    }

    /// <summary>
    /// The IP test.
    /// </summary>
    [TestMethod]
    public void ParseIpv4Test()
    {
        // Arrange
        IPAddress? ip = IPAddressParser.Parse("192.168.0.1");

        // Assert
        Assert.AreEqual("192.168.0.1", ip?.ToString(), "Equal");
    }

    /// <summary>
    /// The IP test.
    /// </summary>
    [TestMethod]
    public void ParseIpv6BracketTest()
    {
        // Arrange
        IPAddress? ip = IPAddressParser.Parse("[1fff:0:a88:85a3::ac1f]");

        // Assert
        Assert.AreEqual("1fff:0:a88:85a3::ac1f", ip?.ToString(), "Equal");
    }

    /// <summary>
    /// The IP local host test.
    /// </summary>
    [TestMethod]
    public void ParseIpv6LocalhostTest()
    {
        // Arrange
        IPAddress? ip = IPAddressParser.Parse("::1");

        // Assert
        Assert.AreEqual("::1", ip?.ToString(), "Equal");
    }

    /// <summary>
    /// The IP port test.
    /// </summary>
    [TestMethod]
    public void ParseIpv6PortTest()
    {
        // Arrange
        IPAddress? ip = IPAddressParser.Parse("[1fff:0:a88:85a3::ac1f]:8001");

        // Assert
        Assert.AreEqual("1fff:0:a88:85a3::ac1f", ip?.ToString(), "Equal");
    }

    /// <summary>
    /// The IP test.
    /// </summary>
    [TestMethod]
    public void ParseIpv6Test()
    {
        // Arrange
        IPAddress? ip = IPAddressParser.Parse("1fff:0:a88:85a3::ac1f");

        // Assert
        Assert.AreEqual("1fff:0:a88:85a3::ac1f", ip?.ToString(), "Equal");
    }

    #endregion
}