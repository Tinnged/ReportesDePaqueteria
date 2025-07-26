using Microsoft.Extensions.Logging;
using ReportesDePaqueteria.MVVM.ViewModels;
using ReportesDePaqueteria.MVVM.Views;

namespace ReportesDePaqueteria
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

            // Register ViewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<ShipmentsViewModel>();
            builder.Services.AddTransient<IncidentsViewModel>();
            builder.Services.AddTransient<TrackingViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<CreateShipmentViewModel>();
            builder.Services.AddTransient<CreateIncidentViewModel>();

            // Register Views
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<ShipmentsView>();
            builder.Services.AddTransient<IncidentsView>();
            builder.Services.AddTransient<TrackingView>();
            builder.Services.AddTransient<ProfileView>();
            builder.Services.AddTransient<CreateShipmentView>();
            builder.Services.AddTransient<CreateIncidentView>();
            builder.Services.AddTransient<ShipmentDetailsView>();
            builder.Services.AddTransient<IncidentDetailsView>();
            builder.Services.AddTransient<IncidentHistoryView>();

            // Register Services (when you add them later)
            // builder.Services.AddSingleton<IDataService, DataService>();
            // builder.Services.AddSingleton<INavigationService, NavigationService>();

            return builder.Build();
        }
    }
}
