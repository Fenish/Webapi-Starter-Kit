# Session Authentication

Simple session authentication using [Entity Framework Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/8.0.0-rc.2.23480.1) with Power of [PostgreSql](https://www.postgresql.org/).<br>
Using [Npgsql.EntityFrameworkCore.PostgreSQL](https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL/) package.

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

### Details

* ### Endpoint list <br>
  ![](https://i.imgur.com/Z822MwO.png)
