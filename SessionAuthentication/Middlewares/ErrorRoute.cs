namespace SessionAuthentication.Middlewares;

public class ErrorRoute
{
    private readonly RequestDelegate _next;
    public ErrorRoute(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {   
        var endpoint = context.GetEndpoint();
        if (endpoint != null)
        {
            await _next(context);
            return;
        }
        
        context.Response.StatusCode = 404;
        await context.Response.WriteAsJsonAsync(new { message = "Route not found" });
        
    }
}