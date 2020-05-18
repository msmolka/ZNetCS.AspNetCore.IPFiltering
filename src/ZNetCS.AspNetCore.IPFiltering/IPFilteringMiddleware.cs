// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPFilteringMiddleware.cs" company="Marcin Smółka zNET Computer Solutions">
//   Copyright (c) Marcin Smółka zNET Computer Solutions. All rights reserved.
// </copyright>
// <summary>
//   The IP filtering middleware.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ZNetCS.AspNetCore.IPFiltering
{
    #region Usings

    using System;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    #endregion

    /// <summary>
    /// The IP filtering middleware.
    /// </summary>
    public class IPFilteringMiddleware : IDisposable
    {
        #region Fields

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The next request delegate.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// The options.
        /// </summary>
        private readonly IPFilteringOptions options;

        /// <summary>
        /// The options disposable.
        /// </summary>
        private readonly IDisposable optionsDisposable;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IPFilteringMiddleware"/> class.
        /// </summary>
        /// <param name="next">
        /// The <see cref="RequestDelegate"/> representing the next middleware in the pipeline.
        /// </param>
        /// <param name="loggerFactory">
        /// The logger factory.
        /// </param>
        /// <param name="options">
        /// The <see cref="IPFilteringOptions"/> representing the options for the <see cref="IPFilteringMiddleware"/>.
        /// </param>
        public IPFilteringMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IOptionsMonitor<IPFilteringOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            this.next = next;
            this.logger = loggerFactory.CreateLogger<IPFilteringMiddleware>();

            this.options = options.CurrentValue;

            this.optionsDisposable = options.OnChange(
                opts =>
                {
                    this.options.DefaultBlockLevel = opts.DefaultBlockLevel;
                    this.options.HttpStatusCode = opts.HttpStatusCode;
                    this.options.Blacklist = opts.Blacklist;
                    this.options.Whitelist = opts.Whitelist;
                    this.options.IgnoredPaths = opts.IgnoredPaths;
                    this.options.PathOptions = opts.PathOptions;
                });
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Invokes middleware.
        /// </summary>
        /// <param name="context">
        /// The <see cref="HttpContext"/> context.
        /// </param>
        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var finder = context.RequestServices.GetRequiredService<IIPAddressFinder>();
            var checker = context.RequestServices.GetRequiredService<IIPAddressChecker>();

            string path = context.Request.Path.HasValue ? context.Request.Path.Value : null;
            string verb = context.Request.Method;

            PathOption foundPath = null;

            if (this.options.PathOptions != null)
            {
                this.logger.LogDebug("Checking if path: {path} with method {verb} is on any specific path option.", path, verb);

                foreach (PathOption pathOption in this.options.PathOptions)
                {
                    if (checker.CheckPaths(verb, path, pathOption.Paths))
                    {
                        foundPath = pathOption;
                        break;
                    }
                }
            }

            IPAddress ipAddress = finder.Find(context);

            if (foundPath != null)
            {
                this.logger.LogDebug("Checking if IP: {ipAddress} address is allowed .", ipAddress);
                if (checker.IsAllowed(ipAddress, foundPath.ParsedWhitelist, foundPath.ParsedBlacklist, foundPath.DefaultBlockLevel))
                {
                    this.logger.LogDebug("IP is allowed for further process.");
                    await this.next.Invoke(context);
                }
                else
                {
                    this.logger.LogInformation(1, "The IP Address: {ipAddress} was blocked.", ipAddress);
                    context.Response.StatusCode = (int)foundPath.HttpStatusCode;
                }
            }
            else
            {
                this.logger.LogDebug("Checking if path: {path} with method {verb} should be ignored or IP: {ipAddress} address is allowed .", path, verb, ipAddress);

                if (checker.CheckPaths(verb, path, this.options.IgnoredPaths)
                    || checker.IsAllowed(ipAddress, this.options.ParsedWhitelist, this.options.ParsedBlacklist, this.options.DefaultBlockLevel))
                {
                    this.logger.LogDebug("IP is allowed for further process.");
                    await this.next.Invoke(context);
                }
                else
                {
                    this.logger.LogInformation(1, "The IP Address: {ipAddress} was blocked.", ipAddress);
                    context.Response.StatusCode = (int)this.options.HttpStatusCode;
                }
            }
        }

        #endregion

        #region Implemented Interfaces

        #region IDisposable

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// True if managed resources should be disposed; otherwise, false.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.optionsDisposable?.Dispose();
            }
        }

        #endregion
    }
}