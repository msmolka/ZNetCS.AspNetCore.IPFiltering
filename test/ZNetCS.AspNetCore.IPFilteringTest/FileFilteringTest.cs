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
        /// The block none test.
        /// </summary>
        [TestMethod]
        public async Task FileAllowAllNotFoundTest()
        {
            using (var server = new TestServer(WebHostBuilderHelper.CreateAllowFileBuilder()))
            {
                // Act
                RequestBuilder request = server.CreateRequest("/");
                request.AddHeader("X-Real-IP", "192.168.0.1");

                HttpResponseMessage response = await request.SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "StatusCode != OK");
            }
        }

        /// <summary>
        /// The file allow not on blacklist test.
        /// </summary>
        [TestMethod]
        public async Task FileAllowRealIPNotOnBlacklistTest()
        {
            using (var server = new TestServer(WebHostBuilderHelper.CreateAllowFileBuilder()))
            {
                // Act
                RequestBuilder request = server.CreateRequest("/");
                request.AddHeader("X-Real-IP", "192.168.0.1");

                HttpResponseMessage response = await request.SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "StatusCode != OK");
            }
        }

        /// <summary>
        /// The file allow on blacklist but whitelisted test.
        /// </summary>
        [TestMethod]
        public async Task FileAllowRealIPOnBlacklistBTest()
        {
            using (var server = new TestServer(WebHostBuilderHelper.CreateAllowFileBuilder()))
            {
                // Act
                RequestBuilder request = server.CreateRequest("/");
                request.AddHeader("X-Real-IP", "192.168.0.101");

                HttpResponseMessage response = await request.SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "StatusCode != OK");
            }
        }

        /// <summary>
        /// The file allow on whitelist blacklist test.
        /// </summary>
        [TestMethod]
        public async Task FileAllowRealIPOnWhitelistBlacklistTest()
        {
            using (var server = new TestServer(WebHostBuilderHelper.CreateBlockFileBuilder()))
            {
                // Act
                RequestBuilder request = server.CreateRequest("/");
                request.AddHeader("X-Real-IP", "192.168.0.10");

                HttpResponseMessage response = await request.SendAsync("PUT");

                // Assert
                response.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "StatusCode != OK");
            }
        }

        /// <summary>
        /// The block all test.
        /// </summary>
        [TestMethod]
        public async Task FileBlockAllNotFoundTest()
        {
            using (var server = new TestServer(WebHostBuilderHelper.CreateBlockFileBuilder()))
            {
                // Act
                RequestBuilder request = server.CreateRequest("/");
                request.AddHeader("X-Real-IP", "192.168.0.1");

                HttpResponseMessage response = await request.SendAsync("PUT");

                Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode, "StatusCode != Unauthorized");
            }
        }

        /// <summary>
        /// The file block on blacklist test.
        /// </summary>
        [TestMethod]
        public async Task FileBlockRealIPOnBlacklistTest()
        {
            using (var server = new TestServer(WebHostBuilderHelper.CreateAllowFileBuilder()))
            {
                // Act
                RequestBuilder request = server.CreateRequest("/");
                request.AddHeader("X-Real-IP", "192.168.0.10");

                HttpResponseMessage response = await request.SendAsync("PUT");

                // Assert                
                Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode, "StatusCode != Unauthorized");
            }
        }

        /// <summary>
        /// The file block on whitelist but blacklisted test.
        /// </summary>
        [TestMethod]
        public async Task FileBlockRealIPOnWhitelistBlacklistTest()
        {
            using (var server = new TestServer(WebHostBuilderHelper.CreateBlockFileBuilder()))
            {
                // Act
                RequestBuilder request = server.CreateRequest("/");
                request.AddHeader("X-Real-IP", "192.168.0.120");

                HttpResponseMessage response = await request.SendAsync("PUT");

                // Assert
                Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode, "StatusCode != Not Found");
            }
        }

        #endregion
    }
}