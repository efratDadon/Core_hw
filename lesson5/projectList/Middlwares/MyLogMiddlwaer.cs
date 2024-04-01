using System.Diagnostics;
using System.Text.Json;

namespace projectList.Middlwares;

public class MyLogMiddleware
{
    private string filePath;
    private readonly RequestDelegate next;

    public MyLogMiddleware(RequestDelegate next, IWebHostEnvironment webHost)
    {
        this.filePath = Path.Combine(webHost.ContentRootPath, "Logs", "log.txt");
        using (var jsonFile = File.OpenText(filePath))
        {
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
        this.next = next;
    }

    public async Task Invoke(HttpContext c)
    {
        var sw = new Stopwatch();
        sw.Start();
        await next.Invoke(c);
        sw.Stop();
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }
        File.AppendAllText(filePath, JsonSerializer.Serialize($"{c.Request.Path}.{c.Request.Method} took {sw.ElapsedMilliseconds}ms."
            + $" User: {c.User?.FindFirst("userId")?.Value ?? "unknown"}") + "\n");

    }
}

public static partial class MiddlewareExtensions
{
    public static IApplicationBuilder UseMyLogMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MyLogMiddleware>();
    }
}