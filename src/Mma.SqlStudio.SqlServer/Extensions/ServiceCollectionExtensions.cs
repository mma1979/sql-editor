using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Mma.SqlStudio.SqlServer.Models;
using Mma.SqlStudio.SqlServer.Services;
using System;

namespace Mma.SqlStudio.SqlServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlStudio(this IServiceCollection services, Action<SqlStudioOptions> configureOptions)
        {
            if (configureOptions == null) throw new ArgumentNullException(nameof(configureOptions));

            services.Configure(configureOptions);

            services.AddOptions<RazorPagesOptions>()
                .Configure<IOptions<SqlStudioOptions>>((options, sqlStudioOptions) =>
                {
                    options.Conventions.Add(new SqlStudioPageRouteModelConvention(sqlStudioOptions.Value.Route));
                    
                    if (sqlStudioOptions.Value.AuthFilter is not null)
                    {
                        options.Conventions.AddPageApplicationModelConvention(
                            "/SqlStudio",
                            model => model.Filters.Add(
                                new Filters.SqlStudioAuthPageFilter(sqlStudioOptions.Value.AuthFilter, sqlStudioOptions.Value.UnauthorizedRedirectUrl)
                            )
                        );
                    }
                });

            services.AddScoped<SchemaService>();

            return services;
        }

        public static IServiceCollection AddSqlStudio(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.Configure<SqlStudioOptions>(configuration.GetSection("SqlStudio"));

            services.AddOptions<RazorPagesOptions>()
                .Configure<IOptions<SqlStudioOptions>>((options, sqlStudioOptions) =>
                {
                    options.Conventions.Add(new SqlStudioPageRouteModelConvention(sqlStudioOptions.Value.Route));

                    if (sqlStudioOptions.Value.AuthFilter is not null)
                    {
                        options.Conventions.AddPageApplicationModelConvention(
                            "/SqlStudio",
                            model => model.Filters.Add(
                                new Filters.SqlStudioAuthPageFilter(sqlStudioOptions.Value.AuthFilter, sqlStudioOptions.Value.UnauthorizedRedirectUrl)
                            )
                        );
                    }
                });

            services.AddScoped<SchemaService>();

            return services;
        }
    }
}
