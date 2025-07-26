using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReportesDePaqueteria.MVVM.Models; 

namespace ReportesDePaqueteria.MVVM.ViewModels
{
    public class CreateIncidentViewModel : BaseViewModel
    {
        private ObservableCollection<Shipment> _shipments;
        public ObservableCollection<Shipment> Shipments
        {
            get => _shipments;
            set => SetProperty(ref _shipments, value);
        }

        private Shipment _selectedShipment;
        public Shipment SelectedShipment
        {
            get => _selectedShipment;
            set => SetProperty(ref _selectedShipment, value);
        }

        private string _category;
        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _priority = "Media";
        public string Priority
        {
            get => _priority;
            set => SetProperty(ref _priority, value);
        }

        public List<string> Categories { get; } = new List<string>
        {
            "Retraso en entrega",
            "Paquete dañado",
            "Paquete extraviado",
            "Error en dirección",
            "Problema con transportista",
            "Otro"
        };

        public List<string> Priorities { get; } = new List<string>
        {
            "Alta",
            "Media",
            "Baja"
        };

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public CreateIncidentViewModel()
        {
            Title = "Reportar Nuevo Incidente";
            Shipments = new ObservableCollection<Shipment>();

            SaveCommand = new Command(async () => await SaveIncidentAsync());
            CancelCommand = new Command(async () => await CancelAsync());

            LoadShipments();
        }

        private void LoadShipments()
        {
            // Load user's shipments (simulated)
            Shipments.Add(new Shipment
            {
                Code = "ENV-2025-001",
                Route = "San José → Cartago",
                Status = "En Ruta"
            });

            Shipments.Add(new Shipment
            {
                Code = "ENV-2025-045",
                Route = "Heredia → Alajuela",
                Status = "En Proceso"
            });
        }

        private async Task SaveIncidentAsync()
        {
            // Validate fields
            if (SelectedShipment == null)
            {
                await Shell.Current.DisplayAlert("Error",
                    "Por favor seleccione un envío", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Category))
            {
                await Shell.Current.DisplayAlert("Error",
                    "Por favor seleccione una categoría", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                await Shell.Current.DisplayAlert("Error",
                    "Por favor describa el incidente", "OK");
                return;
            }

            if (IsBusy) return;

            IsBusy = true;

            try
            {
                // Simulate API call
                await Task.Delay(1500);

                var newIncident = new Incident
                {
                    Code = $"INC-2025-{Random.Shared.Next(100, 999)}",
                    Title = Category,
                    Description = Description,
                    Category = Category,
                    Priority = Priority,
                    Status = "Abierto",
                    Date = DateTime.Now,
                    ShipmentCode = SelectedShipment.Code
                };

                await Shell.Current.DisplayAlert("Éxito",
                    $"Incidente reportado correctamente\nCódigo: {newIncident.Code}", "OK");

                // Navigate back
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error",
                    $"Error al reportar incidente: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CancelAsync()
        {
            var confirm = await Shell.Current.DisplayAlert("Cancelar",
                "¿Está seguro que desea cancelar el reporte?", "Sí", "No");

            if (confirm)
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
