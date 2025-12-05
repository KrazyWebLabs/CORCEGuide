using CommunityToolkit.Maui;
using CORCEGuideApp.Repositories;
using CORCEGuideApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CORCEGuideApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit(options =>
                {
                    options.SetShouldEnableSnackbarOnWindows(true);
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            var dbPath = Helpers.FileAccessHelper.GetLocalFilePath("CORCEGuideApp.db");

            builder.Services.AddDbContext<Models.DataContext>(options =>
            {
                options.UseSqlite($"Data Source={dbPath}");
            });

            builder.Services.AddSingleton<CategoryViewModel>();
            builder.Services.AddSingleton<RoleViewModel>();
            builder.Services.AddSingleton<UserViewModel>();
            builder.Services.AddSingleton<AnnouncementViewModel>();
            builder.Services.AddSingleton<FavoriteViewModel>();

            builder.Services.AddSingleton<FavoriteRepository>();
            builder.Services.AddSingleton<CategoryRepositoy>();
            builder.Services.AddSingleton<RoleReposity>();
            builder.Services.AddSingleton<UserRepository>();
            builder.Services.AddSingleton<AnnouncementRepository>();

            var app = builder.Build();

            using ( var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Models.DataContext>();
                db.Database.EnsureCreated();
            }

            return app;
        }
    }
}
