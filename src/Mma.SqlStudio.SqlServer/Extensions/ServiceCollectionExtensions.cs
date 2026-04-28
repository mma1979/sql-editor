using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
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

            services.AddScoped<SchemaService>();

            return services;
        }

        public static IServiceCollection AddSqlStudio(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.Configure<SqlStudioOptions>(configuration.GetSection("SqlStudio"));

            services.AddScoped<SchemaService>();

            return services;
        }
    }
}
