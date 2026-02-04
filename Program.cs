using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Nostdlib;
using Nostdlib.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IDataService, JsonDataService>();
builder.Services.AddScoped<ILocalizationService, LocalizationService>();
builder.Services.AddLocalization();

var host = builder.Build();

// Initialize localization service to load saved language preference
var localizationService = host.Services.GetRequiredService<ILocalizationService>() as LocalizationService;
if (localizationService != null)
{
    await localizationService.InitializeAsync();
}

await host.RunAsync();
