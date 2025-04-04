using BlazorCourse;
using BlazorCourse.Components;
using BlazorCourse.Components.Pages.Components.ParentChildServiceExample;
using BlazorCourse.Services;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Radzen;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddHubOptions(options => { options.MaximumReceiveMessageSize = 1024 * 1024; });

builder.Services.AddScoped<CounterService>();
builder.Services.AddScoped<MessageService>();

builder.Services.AddSyncfusionBlazor();
var syncfusionLicenseKey = Environment.GetEnvironmentVariable("SYNCFUSION_LICENSE");
Console.WriteLine("syncfusionLicenseKey: " + syncfusionLicenseKey);
if (!string.IsNullOrWhiteSpace(syncfusionLicenseKey)) SyncfusionLicenseProvider.RegisterLicense(syncfusionLicenseKey);

builder.Services.AddRadzenComponents();

builder.Services
    .AddBlazorise(options => { options.Immediate = true; })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

builder.Services.AddAntiforgery(options => { options.SuppressXFrameOptionsHeader = true; });

var app = builder.Build();

ConfigurationHelper.Configuration = app.Configuration;

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Frame-Options", "ALLOWALL");
    await next();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();