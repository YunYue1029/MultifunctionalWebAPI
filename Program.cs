var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:80");

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");

app.MapGet("/", () => Results.Json(new { message = "Welcome to MyApi!" }));
app.MapControllers();

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 404)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\": \"API not found\"}");
    }
});

app.Run();