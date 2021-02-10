# ASP .NET + MySQL Template Project

This project provides a starting point for making a web app with ASP .NET 5 and
MySQL 8 with EF Core 5.

## Prerequisites

-   [Download the .NET 5.0 SDK](https://dotnet.microsoft.com/download)
-   [Download MySQL](https://dev.mysql.com/downloads/)
-   (Optional) Run `dotnet dev-certs https --trust` in your terminal so that your
    browser doesn't think the development server is unsafe
    -   You may have to close and reopen your browser to let this take effect
    -   Note: this isn't supported on Linux
-   Create a new MySQL user `myprojectlocaluser` with password `myprojectlocalpw`,
    see
    [here](https://dev.mysql.com/doc/workbench/en/wb-mysql-connections-navigator-management-users-and-privileges.html)
    -   Give the user the "DBA" administrative role

## Development

-   Run `dotnet watch run` to start the development server

### Updating the Database Schema

-   Make any changes to the model files
-   Ensure the EF command line tool is installed by running `dotnet tool install --global dotnet-ef`
-   Drop the current database `dotnet ef database drop --force`
-   Update the database seeding code in `Data/DbInitializer.cs` if necessary
-   Restart the app

Note: it's also possible to use [EF
Migrations](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/migrations?view=aspnetcore-5.0&tabs=visual-studio-code),
but this app is just using fake seeded data so there's no need.

## Initial Project Creation

The overall steps used to create this project are as follows:

1. Run `dotnet new webapp -o AspDotNetMySqlTemplate` in the terminal
2. Add the `Models` folder with `Example.cs`
3. Install packages necessary to generate code based on models:

    ```shell
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 5.0.0-*
    dotnet add package Microsoft.EntityFrameworkCore.Design -v 5.0.0-*
    dotnet add package Microsoft.EntityFrameworkCore.Tools -v 5.0.0-*
    dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design -v 5.0.0-*
    dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore -v 5.0.0-*
    ```

4. Install [MySQL EF Core provider](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql):

    ```shell
    dotnet add package Pomelo.EntityFrameworkCore.MySql -v 5.0.0-alpha.2
    ```

5. Install scaffolding tool:

    ```shell
    dotnet tool uninstall --global dotnet-aspnet-codegenerator
    dotnet tool install --global dotnet-aspnet-codegenerator --version 5.0.0-*
    ```

6. Run:

    ```shell
    dotnet aspnet-codegenerator razorpage -m Example -dc AspDotNetMySqlTemplate.Data.ExampleContext -udl -outDir Pages\Examples --referenceScriptLibraries
    ```

    which:

    - Creates Razor pages in the Pages/Examples folder:
        - Create.cshtml and Create.cshtml.cs
        - Delete.cshtml and Delete.cshtml.cs
        - Details.cshtml and Details.cshtml.cs
        - Edit.cshtml and Edit.cshtml.cs
        - Index.cshtml and Index.cshtml.cs
    - Creates Data/ExampleContext.cs.
    - Adds the context to dependency injection in Startup.cs (for SQL Server).
    - Adds a database connection string to appsettings.json (for SQL Server).

7. Add `AddDatabaseDeveloperPageExceptionFilter` to `ConfigureServices` in
   `Startup.cs` to help with database debugging

8. Update `appsettings.json` to use the connection string:

    ```json
    "MySqlConnection": "server=localhost; port=3306; database=example; user=myprojectlocaluser; password=myprojectlocalpw; Persist Security Info=False; Connect Timeout=300"
    ```

    You can replace the username and password with whatever. This isn't a secure
    method, and it should not be used for deployment. With multiple people
    working on the same project this is convenient though.

9. Update `ConfigureServices` in `Startup.cs` to use

    ```c#
    string connectionString = Configuration.GetConnectionString("MySqlConnection");
    services.AddDbContext<ExampleContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    );
    ```

    instead of the previous SQL Server DB context

10. Update `Program.cs` to initialize the database on startup with:

    ```c#
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        CreateDbIfNotExists(host);

        host.Run();
    }
    private static void CreateDbIfNotExists(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<Data.ExampleContext>();
                Data.DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }
    ```

    to set up and seed the database each time the app starts

11. Add `Data/DbInitializer.cs` to take care of DB setup and seeding

12. Add `.gitignore` from [here](https://stackoverflow.com/a/39289838/14703577)

## VSCode Tips

-   Install the C# extension (it's a workspace recommendation in this template)
    -   See [here](https://code.visualstudio.com/docs/languages/csharp) for info
        about C# in VSCode
-   When first opening the project VSCode will ask for permission to generate
    assets for building and debugging, approve this. If you miss this prompt, run
    the command `.NET: Generate Assets for Build and Debug` in the command palette.
-   Add the following to your `settings.json` to use Emmet in C# HTML files
    ```json
    "emmet.includeLanguages": {
        "aspnetcorerazor": "html"
    }
    ```
-   Semantic highlighting for C# can be enabled in the VSCode settings.
-   This project comes with a few VSCode Tasks for various development tasks, such
    as starting the dev server (the `watch` task)
-   The project also has launch definitions for debugging the application

## Resources

-   [EF Core with ASP .NET Tutorial](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-5.0&tabs=visual-studio)
    -   This project is based heavily off of this tutorial
    -   If you're using Visual Studio, that tutorial has instructions specific to
        Visual Studio that might be helpful
-   [ASP .NET Getting Started](https://docs.microsoft.com/en-us/aspnet/core/getting-started/?view=aspnetcore-5.0&tabs=windows)
