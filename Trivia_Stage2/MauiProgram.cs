using Microsoft.Extensions.Logging;
using Trivia_Stage2.Services;
using Trivia_Stage2.Views;
using Trivia_Stage2.ViewModels;

namespace Trivia_Stage2
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
            .AddTransient<ApproveQuestionsPage>()

            //Registering ViewModels
            .AddTransient<ApproveQuestionsPageViewModel>()
            .AddTransient<BestScoresPageViewModel>()
            .AddTransient<UserAdminPageViewModel>()
            .AddTransient<UserQuestionsPageViewModel>()
            .AddTransient<LoginPageViewModel>()
            .AddTransient<ApproveQuestionsPageViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}