# RecipEase

This is the RecipEase project for CPSC 471 Group 14.

- [RecipEase](#recipease)
  - [Prerequisites](#prerequisites)
  - [Development](#development)
    - [Updating the Database Schema](#updating-the-database-schema)
  - [Initial Project Creation](#initial-project-creation)
  - [VSCode Tips](#vscode-tips)
  - [Resources](#resources)

## Prerequisites

-   [Download the .NET 5.0 SDK](https://dotnet.microsoft.com/download)
-   [Download MySQL](https://dev.mysql.com/downloads/)
-   (Optional) Run `dotnet dev-certs https --trust` in your terminal so that your
    browser doesn't think the development server is unsafe
    -   You may have to close and reopen your browser to let this take effect
    -   Note: this isn't supported on Linux
-   Create a new MySQL user `recipeaselocaluser` with password `recipeaselocalpw`,
    see
    [here](https://dev.mysql.com/doc/workbench/en/wb-mysql-connections-navigator-management-users-and-privileges.html)
    -   Give the user the "DBA" administrative role

## Development

-   Run `dotnet watch --project ./Server/RecipEase.Server.csproj run` to start the development server
    -   Make sure this is run from the project root
    -   Alternatively, run the `watch` task from VSCode

### Updating the Database Schema

-   Make any changes to the model files
-   Ensure the EF command line tool is installed by running `dotnet tool install --global dotnet-ef`
-   Drop the current database `dotnet ef database drop --force`
-   Update the database seeding code in `Server/Data/DbInitializer.cs` if necessary
-   Restart the app

Note: it's also possible to use [EF
Migrations](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/migrations?view=aspnetcore-5.0&tabs=visual-studio-code),
but this app is just using fake seeded data so there's no need.

## Initial Project Creation

See
[here](https://github.com/rynoV/AspDotNetMySqlTemplate#initial-project-creation)
for details on how the project was initially scaffolded.
    
For adding Blazor WebAssembly the template code from the command `dotnet new
blazorwasm -ho -o AppName` was used and integrated with the code from the above
template.

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
    -   This project is based off of this tutorial
    -   If you're using Visual Studio, that tutorial has instructions specific to
        Visual Studio that might be helpful
-   [ASP .NET Getting Started](https://docs.microsoft.com/en-us/aspnet/core/getting-started/?view=aspnetcore-5.0&tabs=windows)
-   [Debugging Blazor WASM](https://docs.microsoft.com/en-us/aspnet/core/blazor/debug?view=aspnetcore-5.0&tabs=visual-studio-code)
-   [Call Web API from Blazor WASM](https://docs.microsoft.com/en-us/aspnet/core/blazor/call-web-api?view=aspnetcore-5.0)
-   [Blazor WASM form validation](https://docs.microsoft.com/en-us/aspnet/core/blazor/forms-validation?view=aspnetcore-5.0)
-   [Web API apps in .NET](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-5.0)
