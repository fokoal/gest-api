using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace gesteco.api.Configuration
{
    // ReSharper disable once InconsistentNaming
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
        {
            if (!(app.ApplicationServices.GetService(typeof(ILoggerFactory)) is ILoggerFactory loggerFactory))
                throw new Exception(nameof(loggerFactory));

            var logger = loggerFactory.CreateLogger(typeof(Startup).FullName);
            var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var options = serviceProvider.GetRequiredService<CorsOptions>();

                if (options.AllowAny)
                {
                    app.UseCors(builder =>
                    {
                        if (options.AllowWithOrigins.Any(o => !string.IsNullOrEmpty(o)))
                        {
                            logger.LogDebug("Using CORS {0} {1}", "WithOrigins", options.AllowWithOrigins);
                            builder.WithOrigins(options.AllowWithOrigins.ToArray());
                        }

                        if (options.AllowAnyOrigin)
                        {
                            logger.LogDebug("Using CORS {0}", "AllowAnyOrigin");
                            builder.AllowAnyOrigin();
                        }

                        if (options.AllowAnyHeader)
                        {
                            logger.LogDebug("Using CORS {0}", "AllowAnyHeader");
                            builder.AllowAnyHeader();
                        }

                        if (options.AllowAnyMethod)
                        {
                            logger.LogDebug("Using CORS {0}", "AllowAnyMethod");
                            builder.AllowAnyMethod();
                        }
                    });
                }
                else
                {
                    logger.LogDebug("CORS not set");
                }
            }

            return app;
        }
    }
}