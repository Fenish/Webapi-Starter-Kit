# Session Authentication

Simple session authentication using Entity Framework Core with Power of [PostgreSql](https://www.postgresql.org/).<br>
Using [Npgsql.EntityFrameworkCore.PostgreSQL](https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL/) package.

### Note

I used .NET 8 in this project. You can build to machine code using [dotnet publish](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish?tabs=netcore21) command.<br>
After you build your project, you can run it on any machine without .NET installed.

### How to use

1. Clone this repository
2. Build project:
   * Open terminal in project directory
   * Linux: `dotnet publish -c Release -r linux-x64`
   * Windows: `dotnet publish -c Release -r win-x64`
   * Mac: `dotnet publish -c Release -r osx-x64`
   * You can find more RIDs [here](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog)
3. Go to `bin/Release/netcoreapp3.1/{RID}/publish/`
4. Copy .env.example to .env and fill it  
5. Open your browser and go to `localhost:<PORT>` (default port is 5000)