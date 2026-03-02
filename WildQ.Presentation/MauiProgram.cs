using Microsoft.Extensions.Logging;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;

namespace WildQ.Presentation
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

            //Dependency Injection
            builder.Services.AddScoped<IAnimalService, AnimalService>();

            builder.Services.AddScoped<ISearchAnimalService, SearchAnimalService>();

            return builder.Build();
        }
    }
}
