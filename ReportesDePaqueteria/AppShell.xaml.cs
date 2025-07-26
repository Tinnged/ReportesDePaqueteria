using ReportesDePaqueteria.MVVM.Views;

namespace ReportesDePaqueteria
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for navigation
            Routing.RegisterRoute("createshipment", typeof(CreateShipmentView));
            Routing.RegisterRoute("createincident", typeof(CreateIncidentView));
            Routing.RegisterRoute("shipmentdetails", typeof(ShipmentDetailsView));
            Routing.RegisterRoute("incidentdetails", typeof(IncidentDetailsView));
            Routing.RegisterRoute("incidenthistory", typeof(IncidentHistoryView));
        }
    }
}
