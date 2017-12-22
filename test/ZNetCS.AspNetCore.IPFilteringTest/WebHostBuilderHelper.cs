// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebHostBuilderHelper.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   Web host builder helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFilteringTest
{
    #region Usings

    using System.IO;
    using System.Reflection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    using ZNetCS.AspNetCore.IPFiltering;
    using ZNetCS.AspNetCore.IPFiltering.DependencyInjection;

    #endregion

    /// <summary>
    /// Web host builder helper.
    /// </summary>
    public static class WebHostBuilderHelper
    {
        #region Public Methods

        /// <summary>
        /// Creates file builder.
        /// </summary>
        public static IWebHostBuilder CreateAllowFileBuilder()
        {
            var path = Path.GetDirectoryName(typeof(StartupAllow).GetTypeInfo().Assembly.Location);

            // ReSharper disable PossibleNullReferenceException
            var di = new DirectoryInfo(path).Parent.Parent.Parent;

            return new WebHostBuilder()
                .UseStartup<StartupAllow>()
                .UseContentRoot(di.FullName);

            // ReSharper restore PossibleNullReferenceException
        }

        /// <summary>
        /// Creates file builder.
        /// </summary>
        public static IWebHostBuilder CreateBlockFileBuilder()
        {
            var path = Path.GetDirectoryName(typeof(StartupBlock).GetTypeInfo().Assembly.Location);

            // ReSharper disable PossibleNullReferenceException
            var di = new DirectoryInfo(path).Parent.Parent.Parent;

            return new WebHostBuilder()
                .UseStartup<StartupBlock>()
                .UseContentRoot(di.FullName);

            // ReSharper restore PossibleNullReferenceException
        }

        /// <summary>
        /// Creates code builder.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public static IWebHostBuilder CreateCodeBuilder(IPFilteringOptions options = null)
        {
            IWebHostBuilder builder = new WebHostBuilder()
                .ConfigureServices(
                    s => s.AddIPFiltering(
                        opts =>
                        {
                            if (options != null)
                            {
                                opts.DefaultBlockLevel = options.DefaultBlockLevel;
                                opts.HttpStatusCode = options.HttpStatusCode;
                                opts.Blacklist = options.Blacklist;
                                opts.Whitelist = options.Whitelist;
                            }
                        }))
                .Configure(
                    app =>
                    {
                        app.UseIPFiltering();
                        app.Run(
                            async c => { await c.Response.WriteAsync("Hello World"); });
                    });

            return builder;
        }

        #endregion
    }
}