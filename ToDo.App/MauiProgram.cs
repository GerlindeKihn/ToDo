using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Refit;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Text;
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
            MauiAppBuilder builder = MauiApp
                .CreateBuilder()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "Inter");
                    fonts.AddFont("OpenSans-Semibold.ttf", "Inter");
                });

            #if DEBUG
		    builder.Logging.AddDebug();
            #endif

            builder.Services.AddScoped<AuthenticationHandler>();
            builder.Services.AddSingleton<SkipCertifacteCheckHandler>();
            builder.Services.AddApiClient<IToDoApiClient>();
            builder.Services.AddApiClient<IUserApiClient>();

            builder.Services.AddScoped<LoginView>();
            builder.Services.AddScoped<LoginViewModel>();

            builder.Services.AddScoped<ToDosView>();
            builder.Services.AddScoped<ToDosViewModel>();

            builder.Services.AddTransient<EditingView>();
            builder.Services.AddTransient<EditingViewModel>();

            builder.Services.AddScoped<ObservableCollection<ToDoDto>>();
            builder.Services.AddScoped<IToDoHolder, ToDoHolder>();
            builder.Services.AddSingleton(SecureStorage.Default);

            return builder.Build();
        }

        private static void AddApiClient<TClient>(this IServiceCollection serviceCollection)
            where TClient : class =>
            serviceCollection.AddRefitClient<TClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://10.0.2.2:7079"))
                .AddHttpMessageHandler<AuthenticationHandler>()
                .ConfigurePrimaryHttpMessageHandler<SkipCertifacteCheckHandler>();
    }
}