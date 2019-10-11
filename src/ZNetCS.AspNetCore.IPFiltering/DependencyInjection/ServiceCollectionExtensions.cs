// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The service collection extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    #region Usings

    using System;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    using ZNetCS.AspNetCore.IPFiltering;
    using ZNetCS.AspNetCore.IPFiltering.Internal;

    #endregion

    /// <summary>
    /// The service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region Public Methods

        /// <summary>
        /// Adds IP filtering services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">
        /// The <see cref="IServiceCollection"/> to add services to.
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/> so that additional calls can be chained.
        /// </returns>
        [Obsolete("Use " + nameof(AddIPFiltering) + " with configure options or configuration section instead")]
        public static IServiceCollection AddIPFiltering(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();

            services.TryAddSingleton<IIPAddressFinder, IPAddressFinder>();
            services.TryAddSingleton<IIPAddressChecker, IPAddressChecker>();

            return services;
        }

        /// <summary>
        /// Adds IP filtering services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">
        /// The <see cref="IServiceCollection"/> to add services to.
        /// </param>
        /// <param name="configurationSection">
        /// The configuration file section.
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/> so that additional calls can be chained.
        /// </returns>
        public static IServiceCollection AddIPFiltering(this IServiceCollection services, IConfiguration configurationSection)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configurationSection == null)
            {
                throw new ArgumentNullException(nameof(configurationSection));
            }

            services.AddOptions();

            services.Configure<IPFilteringOptions>(configurationSection);

            services.TryAddSingleton<IIPAddressFinder, IPAddressFinder>();
            services.TryAddSingleton<IIPAddressChecker, IPAddressChecker>();

            return services;
        }

        /// <summary>
        /// Adds IP filtering services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">
        /// The <see cref="IServiceCollection"/> to add services to.
        /// </param>
        /// <param name="configure">
        /// The <see cref="IPFilteringMiddleware"/> configuration delegate.
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/> so that additional calls can be chained.
        /// </returns>
        public static IServiceCollection AddIPFiltering(this IServiceCollection services, Action<IPFilteringOptions> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            services.AddOptions();

            services.Configure(configure);

            services.TryAddSingleton<IIPAddressFinder, IPAddressFinder>();
            services.TryAddSingleton<IIPAddressChecker, IPAddressChecker>();

            return services;
        }

        #endregion
    }
}