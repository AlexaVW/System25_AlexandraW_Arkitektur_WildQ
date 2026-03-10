using Domain.Entities.Interfaces;
using Microsoft.Extensions.Logging;
using WildQ.Application.Interfaces;
using WildQ.Application.Services;
using WildQ.Infrastructure.Repositories;
using WildQ.Presentation.ViewModels;
using WildQ.Presentation.Views;

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
            builder.Services.AddScoped<IAnimalRepository, AnimalRepositoryMongoDb>();

            builder.Services.AddTransient<SearchAnimalPageViewModel>(); // Creates a new instance
            builder.Services.AddTransient<SearchAnimalPage>();

            builder.Services.AddTransient<EndangeredAnimalQuizViewModel>();
            builder.Services.AddTransient<EndangeredAnimalQuizPage>();

            return builder.Build();
        }
    }
}
