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
app.MapGet("/Static/CSS/index.css", async context =>
{
    // по аналогии со страницей Index настроим на нашем сервере путь до страницы со стилями, чтобы браузер знал, откуда их загружать
    var cssPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "CSS", "index.css");
    var css = await File.ReadAllTextAsync(cssPath);
    await context.Response.WriteAsync(css);
});

app.Run();