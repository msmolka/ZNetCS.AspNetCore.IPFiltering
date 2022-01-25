// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationBuilderExtensions.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   Extension methods for adding the <see cref="IPFilteringMiddleware" /> to an application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

#region Usings

using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using ZNetCS.AspNetCore.IPFiltering;

#endregion

/// <summary>
/// Extension methods for adding the <see cref="IPFilteringMiddleware"/> to an application.
/// </summary>
public static class ApplicationBuilderExtensions
{
    #region Public Methods

    /// <summary>
    /// Adds the <see cref="IPFilteringMiddleware"/> to automatically set filtering block options.
    /// </summary>
    /// <param name="app">
    /// The <see cref="IApplicationBuilder"/> to use filtering on.
    /// </param>
    /// <returns>
    /// The <see cref="IApplicationBuilder"/> so that additional calls can be chained.
    /// </returns>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "OK")]
    public static IApplicationBuilder UseIPFiltering(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        return app.UseMiddleware<IPFilteringMiddleware>();
    }

    /// <summary>
    /// Adds the <see cref="IPFilteringMiddleware"/> to manually set filtering block options.
    /// </summary>
    /// <param name="app">
    /// The <see cref="IApplicationBuilder"/> to use filtering on.
    /// </param>
    /// <param name="options">
    /// The <see cref="IPFilteringOptions"/> to configure the middleware with.
    /// </param>
    /// <returns>
    /// The <see cref="IApplicationBuilder"/> so that additional calls can be chained.
    /// </returns>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "OK")]
    [Obsolete("Use " + nameof(ServiceCollectionExtensions.AddIPFiltering) + " with configure options instead")]
    public static IApplicationBuilder UseIPFiltering(
        this IApplicationBuilder app,
        IPFilteringOptions options)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        return app.UseMiddleware<IPFilteringMiddleware>(Options.Create(options));
    }

    #endregion
}