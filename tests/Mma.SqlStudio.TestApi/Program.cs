using Mma.SqlStudio.SqlServer.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddRazorPages();

builder.Services.AddSqlStudio(options =>
{
    options.Route = "/script-runner";
    options.AppName = "Script Runner";
    options.Database = "LandFeesDB";
    options.ConnectionString = "data source=localhost;initial catalog=LandFeesDB;persist security info=True;TrustServerCertificate=True; user id=sa;password=Abc@1234;MultipleActiveResultSets=True;Max Pool Size=200;";
    options.EnableSchemaLoad = true;
    options.ExcludedSchemas = new List<string> { "HangFire" };
    options.ExcludedObjects = new List<string> { "ApiLogs", "AppUsers" };
    
    // Example Auth Filter: HTTP Basic Auth (admin:password)
    options.AuthFilter = ctx =>
    {
        if (ctx.Request.Headers.TryGetValue("Authorization", out var authHeader) &&
            authHeader.ToString().StartsWith("Basic "))
        {
            var token = authHeader.ToString().Substring("Basic ".Length).Trim();
            // "admin:password" base64 encoded is "YWRtaW46cGFzc3dvcmQ="
            return token == "YWRtaW46cGFzc3dvcmQ=";
        }

        // Setting this header triggers the browser's native login prompt when returning 401
        ctx.Response.Headers.WWWAuthenticate = "Basic realm=\"SqlStudio\"";
        return false;
    };
    
    // Set to null to return a 401 Unauthorized response instead of redirecting
    options.UnauthorizedRedirectUrl = null;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAntiforgery();
app.MapStaticAssets();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.MapSqlStudioEndpoints();

app.Run();
