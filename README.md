# RecipEase

A Recipe Web Application to maximize the inconvenience of cooking at home by automating the recipe-utilization process.

## Development Environment

-   The software development platform/SDK used was [.NET 5.0](https://dotnet.microsoft.com/download)
	-	The SDK includes the runtime so the runtime does not need to be seperately installed
-	The web development framework that the project was hosted on was **ASP.NET Core**
	-	In Visual Studio, you can select the option to host on ASP.NET Core when created a new Blazor WASM Application
-	The client-side framework to develop the front end was **Blazor Web Assembly**
-   The RDBMS used to store the applications data was [MySQL](https://dev.mysql.com/downloads/)
-	To communicate between the source code and the database, [Entity Framework Core](https://www.entityframeworktutorial.net/what-is-entityframework.aspx) was used
	-	Specifically, the [Pomelo EF Core Provider](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql) was used

## Prerequisites to Running the Application
-	[Download the .NET 5.0 SDK](https://dotnet.microsoft.com/download)
-   [Download MySQL](https://dev.mysql.com/downloads/)
-   (Optional) Run `dotnet dev-certs https --trust` in your terminal so that your
    browser doesn't think the development server is unsafe
    -   You may have to close and reopen your browser to let this take effect
    -   Note: this isn't supported on Linux
-   Create a new MySQL user `recipeaselocaluser` with password `recipeaselocalpw`,
    see
    [here](https://dev.mysql.com/doc/workbench/en/wb-mysql-connections-navigator-management-users-and-privileges.html)
    -   Give the user the "DBA" administrative role


## Running the Application
-	Make sure the following commands are run from the project root (One level above the server folder)
-	First drop any potentially existing database by executing the following CLI command `dotnet ef --project ./Server/RecipEase.Server.csproj database drop --force`
	-	Make sure EF command line tool is installed first `dotnet tool install --global dotnet-ef`
	-	This needs to be done since [EF
Migrations](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/migrations?view=aspnetcore-5.0&tabs=visual-studio-code) was not implemented
-   Then run `dotnet watch --project ./Server/RecipEase.Server.csproj run` to start the development server
    -   Alternatively, run the `watch` task from VSCode

# The Rest

### API Development

The API is contained in the `Server` folder and the logic is in the
`Controllers` folder.

Once the dev server is started, go to https://localhost:5001/swagger/index.html
for an interactive API explorer.

#### Controller Generation

First run

```
dotnet tool install -g dotnet-aspnet-codegenerator
```

Then to generate a controller for a model:

```
dotnet aspnet-codegenerator --project ./Server/RecipEase.Server.csproj controller -name <ControllerName> -async -api -m <ModelName> -dc RecipEaseContext -outDir Controllers
```

where you replace `<ControllerName>` with the name of controller class to be
generated, and `<ModelName>` with the name of a model class from `Shared/Models`
to generate the controller for. This will add the new controller to
`Server/Controllers`.

#### API Reference Generation

To generate an API reference PDF, start the dev server, open
`api-ref-generate.html` in your browser, disable CORS (see the link in the HTML
for a suggestion on how to do this), and click the button.

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
