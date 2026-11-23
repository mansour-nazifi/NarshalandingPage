using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.ResponseCompression;
using NarshaStaticSite.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddControllersWithViews();
builder.Services.AddWebEncoders(options =>
{
    options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
});
builder.Services.AddHttpClient();
if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
    });
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

if (!app.Environment.IsDevelopment())
{
    app.UseResponseCompression();
}
app.UseMiddleware<HtmlMinificationMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
