// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileFilteringTest.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The filtering test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFilteringTest
{
    #region Usings

    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.TestHost;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    #endregion

    /// <summary>
    /// The filtering test.
    /// </summary>
    [TestClass]
    public class FilteringTest
    {
        #region Public Methods

        /// <summary>
        /// The allow all not found test.
        /// </summary>
        [TestMethod]
        public async Task FileAllowAllNotFoundTest()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreateAllowFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/");
            request.AddHeader("X-Real-IP", "192.168.0.1");

            HttpResponseMessage response = await request.SendAsync("PUT");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "StatusCode != OK");
        }

        /// <summary>
        /// The allow all not found test.
        /// </summary>
        [TestMethod]
        public async Task FileAllowAllNotFoundTest2()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreatePathBlockFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/");
            request.AddHeader("X-Real-IP", "192.168.0.1");

            HttpResponseMessage response = await request.SendAsync("PUT");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "StatusCode != OK");
        }

        /// <summary>
        /// The file allow not on blacklist test.
        /// </summary>
        [TestMethod]
        public async Task FileAllowRealIPNotOnBlacklistTest()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreateAllowFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/");
            request.AddHeader("X-Real-IP", "192.168.0.1");

            HttpResponseMessage response = await request.SendAsync("PUT");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "StatusCode != OK");
        }

        /// <summary>
        /// The file allow on blacklist test.
        /// </summary>
        [TestMethod]
        public async Task FileAllowRealIPOnBlacklistTest()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreateAllowFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/");
            request.AddHeader("X-Real-IP", "192.168.0.10");

            HttpResponseMessage response = await request.SendAsync("PUT");

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode, "StatusCode != Unauthorized");
        }

        /// <summary>
        /// The file allow on blacklist but whitelisted test.
        /// </summary>
        [TestMethod]
        public async Task FileAllowRealIPOnBlacklistTest2()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreateAllowFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/");
            request.AddHeader("X-Real-IP", "192.168.0.101");

            HttpResponseMessage response = await request.SendAsync("PUT");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "StatusCode != OK");
        }

        /// <summary>
        /// The block all test.
        /// </summary>
        [TestMethod]
        public async Task FileBlockAllNotFoundTest()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreateBlockFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/");
            request.AddHeader("X-Real-IP", "192.168.0.1");

            HttpResponseMessage response = await request.SendAsync("PUT");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode, "StatusCode != Unauthorized");
        }

        /// <summary>
        /// The block all test.
        /// </summary>
        [TestMethod]
        public async Task FileBlockAllNotFoundTest2()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreatePathAllowFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/");
            request.AddHeader("X-Real-IP", "192.168.0.1");

            HttpResponseMessage response = await request.SendAsync("PUT");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode, "StatusCode != Unauthorized");
        }

        /// <summary>
        /// The file block on whitelist but blacklisted test.
        /// </summary>
        [TestMethod]
        public async Task FileBlockRealIPOnWhitelistBlacklistTest()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreateBlockFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/");
            request.AddHeader("X-Real-IP", "192.168.0.120");

            HttpResponseMessage response = await request.SendAsync("PUT");

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode, "StatusCode != Not Found");
        }

        /// <summary>
        /// The ignore all methods test.
        /// </summary>
        [TestMethod]
        public async Task FileIgnoreAllTest()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreateIgnoreFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/ignorepost");
            request.AddHeader("X-Real-IP", "192.168.0.1");

            HttpResponseMessage response = await request.SendAsync("PUT");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "StatusCode != OK");
        }

        /// <summary>
        /// The not ignore block all test.
        /// </summary>
        [TestMethod]
        public async Task FileIgnoreBlockPutTest()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreateIgnoreFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/ignoreget");
            request.AddHeader("X-Real-IP", "192.168.0.1");

            HttpResponseMessage response = await request.SendAsync("PUT");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode, "StatusCode != Unauthorized");
        }

        /// <summary>
        /// The ignore get test.
        /// </summary>
        [TestMethod]
        public async Task FileIgnoreGetTest()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreateIgnoreFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/ignoreget");
            request.AddHeader("X-Real-IP", "192.168.0.1");

            HttpResponseMessage response = await request.SendAsync("GET");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "StatusCode != OK");
        }

        /// <summary>
        /// The path allow all not found test.
        /// </summary>
        [TestMethod]
        public async Task FilePathAllowAllNotFoundTest()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreatePathAllowFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/pathpost");
            request.AddHeader("X-Real-IP", "192.168.0.1");

            HttpResponseMessage response = await request.SendAsync("PUT");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "StatusCode != OK");
        }

        /// <summary>
        /// The path allow not on blacklist test.
        /// </summary>
        [TestMethod]
        public async Task FilePathAllowRealIPNotOnBlacklistTest()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreatePathAllowFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/pathget");
            request.AddHeader("X-Real-IP", "192.168.0.1");

            HttpResponseMessage response = await request.SendAsync("GET");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "StatusCode != OK");
        }

        /// <summary>
        /// The path allow on blacklist test.
        /// </summary>
        [TestMethod]
        public async Task FilePathAllowRealIPOnBlacklistTest()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreatePathAllowFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/pathget");
            request.AddHeader("X-Real-IP", "192.168.0.10");

            HttpResponseMessage response = await request.SendAsync("GET");

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode, "StatusCode != Unauthorized");
        }

        /// <summary>
        /// The path allow on blacklist but whitelisted test.
        /// </summary>
        [TestMethod]
        public async Task FilePathAllowRealIPOnBlacklistTest2()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreatePathAllowFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/pathpost");
            request.AddHeader("X-Real-IP", "192.168.0.101");

            HttpResponseMessage response = await request.SendAsync("POST");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "StatusCode != OK");
        }

        /// <summary>
        /// The path block all test.
        /// </summary>
        [TestMethod]
        public async Task FilePathBlockAllNotFoundTest()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreatePathBlockFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/pathpost");
            request.AddHeader("X-Real-IP", "192.168.0.1");

            HttpResponseMessage response = await request.SendAsync("POST");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode, "StatusCode != Unauthorized");
        }

        /// <summary>
        /// The path block on whitelist but blacklisted test.
        /// </summary>
        [TestMethod]
        public async Task FilePathBlockRealIPOnWhitelistBlacklistTest()
        {
            using var server = new TestServer(WebHostBuilderHelper.CreatePathBlockFileBuilder());

            // Act
            RequestBuilder request = server.CreateRequest("/pathget");
            request.AddHeader("X-Real-IP", "192.168.0.120");

            HttpResponseMessage response = await request.SendAsync("GET");

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode, "StatusCode != Not Found");
        }

        #endregion
    }
}