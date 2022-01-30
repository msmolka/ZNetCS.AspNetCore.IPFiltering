// -----------------------------------------------------------------------
// <copyright file="TestWebApplicationFactory.cs" company="Marcin Smółka">
//   Copyright (c) Marcin Smółka. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFilteringTest;

#region Usings

using System.IO;
using System.Reflection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

#endregion

/// <inheritdoc />
internal class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    /// <inheritdoc/>
    protected override IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder().ConfigureWebHostDefaults(_ => { });

    /// <inheritdoc />
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseStartup<TStartup>();
        builder.UseContentRoot(GetPath() ?? string.Empty);
    }

    /// <summary>
    /// Get root path for test web server.
    /// </summary>
    private static string? GetPath()
    {
        string path = Path.GetDirectoryName(typeof(TStartup).GetTypeInfo().Assembly.Location)!;

        // ReSharper disable PossibleNullReferenceException
        DirectoryInfo? di = new DirectoryInfo(path).Parent?.Parent?.Parent;

        return di?.FullName;
    }
}