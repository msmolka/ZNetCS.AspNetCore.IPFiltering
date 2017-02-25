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
        /// Creates code builder.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public static IWebHostBuilder CreateCodeBuilder(IPFilteringOptions options = null)
        {
            if (options == null)
            {
                options = new IPFilteringOptions();
            }

            IWebHostBuilder builder = new WebHostBuilder()
                .ConfigureServices(s => s.AddIPFiltering())
                .Configure(
                    app =>
                    {
                        app.UseIPFiltering(options);
                        app.Run(
                            async c => { await c.Response.WriteAsync("Hello World"); });
                    });

            return builder;
        }

        /// <summary>
        /// Creates file builder.
        /// </summary>
        public static IWebHostBuilder CreateBlockFileBuilder()
        {
            return new WebHostBuilder()
                .UseStartup<StartupBlock>()
                .UseContentRoot(Directory.GetCurrentDirectory());
        }

        /// <summary>
        /// Creates file builder.
        /// </summary>
        public static IWebHostBuilder CreateAllowFileBuilder()
        {
            return new WebHostBuilder()
                .UseStartup<StartupAllow>()
                .UseContentRoot(Directory.GetCurrentDirectory());
        }

        #endregion
    }
}