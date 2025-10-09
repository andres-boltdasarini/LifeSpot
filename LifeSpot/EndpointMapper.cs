using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;

public static class EndpointMapper
{
    // Загружаем общие элементы один раз при старте приложения
    private static readonly string FooterHtml = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "footer.html"));
    private static readonly string SideBarHtml = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "sidebar.html"));
    private static readonly string sliderHtml = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Views", "Shared", "slider.html"));


    // Маппинг HTML страниц

    public static void MapHtmlPages(this WebApplication app)
    {
        app.MapGet("/", async context =>
        {
            var html = await GetHtmlWithLayout("index.html");
            await context.Response.WriteAsync(html);
        });

        app.MapGet("/testing", async context =>
        {
            var html = await GetHtmlWithLayout("testing.html");
            await context.Response.WriteAsync(html);
        });

        app.MapGet("/about", async context =>
        {
            var html = await GetHtmlWithLayout("about.html");
            await context.Response.WriteAsync(html);
        });
    }


    // Маппинг статических файлов (CSS)
    public static void MapCssFiles(this WebApplication app)
    {
        app.MapGet("/Static/CSS/index.css", async context =>
        {
            var cssPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "CSS", "index.css");
            var css = await File.ReadAllTextAsync(cssPath);
            await context.Response.WriteAsync(css);
        });
    }

// Маппинг JavaScript файлов

    public static void MapJsFiles(this WebApplication app)
    {
        app.MapGet("/Static/JS/index.js", async context =>
        {
            var jsPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "JS", "index.js");
            var js = await File.ReadAllTextAsync(jsPath);
            await context.Response.WriteAsync(js);
        });

        app.MapGet("/Static/JS/testing.js", async context =>
        {
            var jsPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "JS", "testing.js");
            var js = await File.ReadAllTextAsync(jsPath);
            await context.Response.WriteAsync(js);
        });

        app.MapGet("/Static/JS/about.js", async context =>
        {
            var jsPath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "JS", "about.js");
            var js = await File.ReadAllTextAsync(jsPath);
            await context.Response.WriteAsync(js);
        });
    }

    // Маппинг изображений
    public static void MapImageFiles(this WebApplication app)
    {
        app.MapGet("/Static/Images/{imageName}", async context =>
        {
            var imageName = context.Request.RouteValues["imageName"]?.ToString();
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Static", "Images", imageName);
            await context.Response.SendFileAsync(imagePath);
        });
    }


    // Маппинг всех эндпоинтов

    public static void MapAllEndpoints(this WebApplication app)
    {
        app.MapHtmlPages();
        app.MapCssFiles();
        app.MapJsFiles();
        app.MapImageFiles();
    }


    // Вспомогательный метод для загрузки HTML с применением макета

    private static async Task<string> GetHtmlWithLayout(string viewName)
    {
        var viewPath = Path.Combine(Directory.GetCurrentDirectory(), "Views", viewName);
        var htmlTemplate = await File.ReadAllTextAsync(viewPath);
        
        return new StringBuilder(htmlTemplate)
            .Replace("<!--SIDEBAR-->", SideBarHtml)
            .Replace("<!--FOOTER-->", FooterHtml)
                               .Replace("<!--SLIDER-->", sliderHtml)
            .ToString();
    }
}