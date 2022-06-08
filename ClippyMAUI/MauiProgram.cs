using Microsoft.AspNetCore.Components.WebView.Maui;
using ClippyMAUI.Data;
using BlazorClippy;
using ClientLibrary.Services;

namespace ClippyMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddTransient<HttpClient>();
        builder.Services.AddTransient<HTTPClientService>();
		builder.Services.AddScoped<ClippyService>();
		builder.Services.AddSingleton<HttpClient>();
		builder.Services.AddSingleton<HTTPClientService>();
        return builder.Build();
	}
}
