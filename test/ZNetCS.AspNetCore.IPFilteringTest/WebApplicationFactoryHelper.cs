// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApplicationFactoryHelper.cs" company="Marcin Smółka">
//   Copyright (c) Marcin Smółka. All rights reserved.
// </copyright>
// <summary>
//   Web Application Factory helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFilteringTest;

#region Usings

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ZNetCS.AspNetCore.IPFiltering;

#endregion

/// <summary>
/// Web Application Factory helper.
/// </summary>
public static class WebApplicationFactoryHelper
{
    #region Public Methods

    /// <summary>
    /// Creates application factory.
    /// </summary>
    public static WebApplicationFactory<StartupAllow> CreateAllowFactory()
    {
        var factory = new TestWebApplicationFactory<StartupAllow>()
            .WithWebHostBuilder(
                builder =>
                    builder.ConfigureAppConfiguration((_, config) => { config.AddJsonFile("appsettingsAllow.json", false, true); }));

        return factory;
    }

    /// <summary>
    /// Creates application factory.
    /// </summary>
    public static WebApplicationFactory<StartupBlock> CreateBlockFactory()
    {
        var factory = new TestWebApplicationFactory<StartupBlock>()
            .WithWebHostBuilder(
                builder =>
                    builder.ConfigureAppConfiguration((_, config) => { config.AddJsonFile("appsettingsBlock.json", false, true); }));

        return factory;
    }

    /// <summary>
    /// Creates code factory.
    /// </summary>
    /// <param name="options">
    /// The options.
    /// </param>
    public static WebApplicationFactory<Startup> CreateCodeFactory(IPFilteringOptions? options = null)
    {
        var factory = new TestWebApplicationFactory<Startup>()
            .WithWebHostBuilder(
                builder =>
                    builder.ConfigureServices(
                            s => s.AddIPFiltering(
                                opts =>
                                {
                                    if (options == null)
                                    {
                                        return;
                                    }

                                    opts.DefaultBlockLevel = options.DefaultBlockLevel;
                                    opts.HttpStatusCode = options.HttpStatusCode;
                                    opts.Blacklist = options.Blacklist;
                                    opts.Whitelist = options.Whitelist;
                                }))
                        .Configure(
                            app =>
                            {
                                app.UseIPFiltering();
                                app.Run(async c => { await c.Response.WriteAsync("Hello World"); });
                            }));
        return factory;
    }

    /// <summary>
    /// Creates application factory.
    /// </summary>
    public static WebApplicationFactory<StartupIgnore> CreateIgnoreFactory()
    {
        var factory = new TestWebApplicationFactory<StartupIgnore>()
            .WithWebHostBuilder(
                builder =>
                    builder.ConfigureAppConfiguration((_, config) => { config.AddJsonFile("appsettingsIgnore.json", false, true); }));

        return factory;
    }

    /// <summary>
    /// Creates application factory.
    /// </summary>
    public static WebApplicationFactory<StartupPathAllow> CreatePathAllowFactory()
    {
        var factory = new TestWebApplicationFactory<StartupPathAllow>()
            .WithWebHostBuilder(
                builder =>
                    builder.ConfigureAppConfiguration((_, config) => { config.AddJsonFile("appsettingsPathAllow.json", false, true); }));

        return factory;
    }

    /// <summary>
    /// Creates application factory.
    /// </summary>
    public static WebApplicationFactory<StartupPathBlock> CreatePathBlockFactory()
    {
        var factory = new TestWebApplicationFactory<StartupPathBlock>()
            .WithWebHostBuilder(
                builder =>
                    builder.ConfigureAppConfiguration((_, config) => { config.AddJsonFile("appsettingsPathBlock.json", false, true); }));

        return factory;
    }

    #endregion
}