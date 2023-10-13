using dotenv.net;
using Microsoft.OpenApi.Models;
using SessionAuthentication.Database;
using SessionAuthentication.Middlewares;

DotEnv.Load();
var builder = WebApplication.CreateSlimBuilder(args);
string port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

builder.WebHost.UseUrls($"http://*:{port}");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Session Authentication Api", Version = "v1", Description = "A simple example of ASP.NET Core Web API"});
});

// Connect Ef Core to Postgres
// Compare this snippet from Database/DataContext.cs:
builder.Services.AddDbContext<DatabaseContext>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Simple Routes
// Home
app.MapGet("/", () => new
{
    project = "Webapi Starter Kit",
    subproject = "Session Authentication",
    repository = "https://github.com/Fenish/Webapi-Starter-Kit"
});

app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();

// Middleware
app.UseMiddleware<ErrorRoute>();

// Server goes BRRRR
app.Run();
