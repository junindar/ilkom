
using ChatbotMultipleSession.Service;
using Microsoft.Extensions.Logging;

namespace ChatbotMultipleSession
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

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient<IChatService, ChatService>();
            builder.Services.AddSingleton<IChatHistoryService, ChatHistoryService>();
            builder.Services.AddSingleton<IChatSessionService, ChatSessionService>();
            return builder.Build();
        }
    }
}
