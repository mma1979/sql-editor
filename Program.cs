using Mma.SqlStudio.SqlServer.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped<Mma.SqlStudio.SqlServer.Services.EditorService>();
builder.Services.Configure<Mma.SqlStudio.SqlServer.Models.SqlStudioOptions>(builder.Configuration.GetSection("SqlStudio"));
builder.Services.AddScoped<Mma.SqlStudio.SqlServer.Services.SchemaService>();

var app = builder.Build();

/*
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseHttpsRedirection();
*/
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
