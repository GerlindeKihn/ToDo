using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Refit;
using System.Collections.ObjectModel;
using ToDo.App.Helpers;
using ToDo.App.Interfaces;
using ToDo.App.ViewModels;
using ToDo.App.Views;
using ToDo.Contracts.Dtos;

namespace ToDo.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
                h.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
            });

            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
                h.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
            });



            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "Inter");
                    fonts.AddFont("OpenSans-Semibold.ttf", "Inter");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<SkipCertifacteCheckHandler>();
            builder.Services
                .AddRefitClient<IToDoApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://10.0.2.2:7079"))
                .ConfigurePrimaryHttpMessageHandler<SkipCertifacteCheckHandler>();

            builder.Services.AddScoped<ToDosView>();
            builder.Services.AddScoped<ToDosViewModel>();

            builder.Services.AddTransient<EditingView>();
            builder.Services.AddTransient<EditingViewModel>();

            builder.Services.AddScoped<ObservableCollection<ToDoDto>>();

            builder.Services.AddScoped<IToDoHolder, ToDoHolder>();

            return builder.Build();
        }
    }
}