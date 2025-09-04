using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Static")),
    RequestPath = "/Static"
});

app.MapGet("/", async context =>
{
    var viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "index.html");
    var html = await File.ReadAllTextAsync(viewPath);
    await context.Response.WriteAsync(html);
});

app.Run();