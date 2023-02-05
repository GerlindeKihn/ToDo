using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using ToDo.App.Helpers;
using ToDo.App.Interfaces;
using ToDo.App.ViewModels;
using ToDo.App.Views;

namespace ToDo.App
{
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<SkipCertifacteCheckHandler>();
            builder.Services
                .AddRefitClient<IToDoApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://10.0.2.2:7079"))
                .ConfigurePrimaryHttpMessageHandler<SkipCertifacteCheckHandler>();

            builder.Services.AddSingleton<ToDosView>();
            builder.Services.AddSingleton<ToDosViewModel>();

            return builder.Build();
        }
    }
}