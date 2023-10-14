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
4. Copy .env and fill it  
5. Open your browser and go to `localhost:<PORT>` (default port is 5000)

## Detailed explanation

* ### Database connection
  1. Used [Entity Framework Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/8.0.0-rc.2.23480.1) to connect to postgresql database.
  Connection details is coming from .env file.
  2. Create database context to connect database and use it in our controllers. [[File]](https://github.com/Fenish/Webapi-Starter-Kit/blob/dad608c6f909ad81c12668698b2a899b268a7502/SessionAuthentication/Database/DatabaseContext.cs#L6)
  3. Create table model for database [[File]](https://github.com/Fenish/Webapi-Starter-Kit/blob/main/SessionAuthentication/Database/Datasets/User.cs)
      <details>
     <summary>Code</summary>

     ```csharp
     using System.ComponentModel.DataAnnotations;

      namespace SessionAuthentication.Database.Datasets;
      
      public class User
      {
        [Key]
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
         public required string Email { get; set; }
      }
     ```
      </details>
  4. Use model in database context [[File]](https://github.com/Fenish/Webapi-Starter-Kit/blob/dad608c6f909ad81c12668698b2a899b268a7502/SessionAuthentication/Database/DatabaseContext.cs#L12C6-L12C6)
      <details>
     <summary>Code</summary>

     ```csharp
     public required DbSet<User> Users { get; set; }
     ```
      </details>
  5. Add database context to services [[File]](https://github.com/Fenish/Webapi-Starter-Kit/blob/dad608c6f909ad81c12668698b2a899b268a7502/SessionAuthentication/Program.cs#L20)
  6. Register context in controllers [[File]](https://github.com/Fenish/Webapi-Starter-Kit/blob/dad608c6f909ad81c12668698b2a899b268a7502/SessionAuthentication/Controllers/Authentication.cs#L24)
  7. Use context for data transfer [[Example]](https://github.com/Fenish/Webapi-Starter-Kit/blob/dad608c6f909ad81c12668698b2a899b268a7502/SessionAuthentication/Controllers/Authentication.cs#L33)

### Endpoint list <br>
  ![](https://i.imgur.com/Z822MwO.png)