using Microsoft.EntityFrameworkCore;
using SessionAuthentication.Database.Datasets;

namespace SessionAuthentication.Database;

public class DatabaseContext: DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }   
    
    public required DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var port = Environment.GetEnvironmentVariable("DB_PORT");
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var database = Environment.GetEnvironmentVariable("DB_DATABASE");
            
            var connectionString = $"Host={host};Port={port};Username={user};Password={password};Database={database}";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}