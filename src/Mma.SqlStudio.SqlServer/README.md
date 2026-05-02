# Mma.SqlStudio.SqlServer

Mma.SqlStudio.SqlServer is a highly customizable, embeddable SQL Server Object Explorer and Query Editor packaged as a Razor Class Library (RCL). It allows you to easily integrate a full-featured SQL development environment into your .NET 8/9/10 applications.

## Features

- **SQL Object Explorer**: Browse databases, schemas, tables, views, and stored procedures.
- **Query Editor**: Execute queries with syntax highlighting and a results grid.
- **Modern UI**: Clean, responsive, and dynamic interface built with vanilla CSS.
- **Embeddable**: Drop into any ASP.NET Core application via Minimal APIs and Razor Pages.

## Getting Started

1. Install the NuGet package.
2. Register the services in your `Program.cs`:

   ```csharp
   builder.Services.AddRazorPages();
   builder.Services.AddSqlStudio(options => {
       options.Route = "/sql-editor"; // Change to your preferred route
       options.ConnectionString = "YourConnectionString";
       options.Database = "YourDatabase";
   });
   ```

3. Map the endpoints and routes:

   ```csharp
   // Required to serve the embedded CSS and JS files
   app.UseStaticFiles(); // Or app.MapStaticAssets() for .NET 9+
   
   app.MapRazorPages();
   app.MapSqlStudioEndpoints();
   ```

4. Access the studio at `/sql-studio`.
