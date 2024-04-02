﻿using Microsoft.Extensions.Logging;
using Trivia_Stage3.Services;
using Trivia_Stage3.Views;
using Trivia_Stage3.ViewModels;

namespace Trivia_Stage3
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
                })
            //Registering serivces
            .Services.AddSingleton<Service>()

            //Registering Views
            .AddTransient<ApproveQuestionsPage>()
            .AddTransient<BestScoresPage>()
            .AddTransient<UserAdminPage>()
            .AddTransient<UserQuestionsPage>()
            .AddTransient<LoginPage>()
            .AddTransient<EditQuestionsPage>()

            //Registering ViewModels
            .AddTransient<ApproveQuestionsPageViewModel>()
            .AddTransient<BestScoresPageViewModel>()
            .AddTransient<UserAdminPageViewModel>()
            .AddTransient<UserQuestionsPageViewModel>()
            .AddTransient<LoginPageViewModel>()
            .AddTransient<EditQuestionsPageViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}