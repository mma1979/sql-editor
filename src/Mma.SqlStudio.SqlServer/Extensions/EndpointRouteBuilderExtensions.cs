using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Mma.SqlStudio.SqlServer.Services;

namespace Mma.SqlStudio.SqlServer.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapSqlStudioEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var options = endpoints.ServiceProvider.GetRequiredService<Microsoft.Extensions.Options.IOptions<Models.SqlStudioOptions>>().Value;
            var apiPath = $"/api/{options.Route.TrimStart('/')}";
            var group = endpoints.MapGroup(apiPath);

            if (options.AuthFilter is not null)
            {
                group.AddEndpointFilter(new Filters.SqlStudioAuthEndpointFilter(options.AuthFilter, options.UnauthorizedRedirectUrl));
            }

            group.MapGet("/schema", async (SchemaService schemaService) =>
            {
                var schema = await schemaService.GetSchemaAsync();
                return Results.Ok(schema);
            });

            group.MapPost("/query", async (QueryRequest request, SchemaService schemaService) =>
            {
                var result = await schemaService.ExecuteQueryAsync(request.Query);
                return Results.Ok(result);
            });

            return endpoints;
        }
    }

    public class QueryRequest
    {
        public string Query { get; set; } = "";
    }
}
