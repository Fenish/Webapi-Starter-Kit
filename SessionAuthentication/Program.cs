using dotenv.net;
using Microsoft.OpenApi.Models;
using SessionAuthentication.Middlewares;

DotEnv.Load();
var builder = WebApplication.CreateBuilder(args);
string port = Environment.GetEnvironmentVariable("PORT") ?? "5000";

builder.WebHost.UseUrls($"http://*:{port}");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Session Authentication Api", Version = "v1", Description = "A simple example of ASP.NET Core Web API"});
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Simple Routes
// Home
app.MapGet("/", () => new { message = "Welcome to Session Authentication Api" });

app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();

// Middleware
app.UseMiddleware<ErrorRoute>();

app.Run();
