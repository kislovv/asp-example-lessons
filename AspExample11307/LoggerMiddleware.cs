namespace AspExample11307;

public class LoggerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
        await next(context);
        Console.WriteLine($"Response: {context.Response.StatusCode} ({context.Response.ContentType})");
    }
}