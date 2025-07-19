using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Tradez.ServiceLayer.Features;
using Tradez.ServiceLayer.Services;
using Tradez.Shared.DependencyInjection;
using Tradez.Shared.Mediator;

namespace Tradez.Maui
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
                });

            var logger = LoggerFactory.Create(b => b.AddConsole()).CreateLogger("Startup");

            builder.Services.AddAttributedServices(
                assembly: typeof(ServicesAssembly).Assembly,
                env: builder.Services.BuildServiceProvider().GetService<IHostEnvironment>(),
                logger: logger
            );

            builder.Services.AddMediatorServices(typeof(FeaturesAssembly).Assembly);

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
