using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ReportesDePaqueteria.MVVM.Models;

namespace ReportesDePaqueteria.MVVM.ViewModels
{
    public class ShipmentsViewModel : BaseViewModel
    {
        private ObservableCollection<Shipment> _shipments;
        public ObservableCollection<Shipment> Shipments
        {
            get => _shipments;
            set => SetProperty(ref _shipments, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterShipments();
            }
        }

        public ICommand RefreshCommand { get; }
        public ICommand CreateShipmentCommand { get; }
        public ICommand SelectShipmentCommand { get; }

        private ObservableCollection<Shipment> _allShipments;

        public ShipmentsViewModel()
        {
            Title = "Mis Envíos";
            Shipments = new ObservableCollection<Shipment>();
            _allShipments = new ObservableCollection<Shipment>();

            RefreshCommand = new Command(async () => await LoadShipmentsAsync());
            CreateShipmentCommand = new Command(async () => await CreateShipmentAsync());
            SelectShipmentCommand = new Command<Shipment>(async (shipment) => await SelectShipmentAsync(shipment));

            LoadShipmentsAsync();
        }

        private async Task LoadShipmentsAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                await Task.Delay(500); // Simulate loading

                _allShipments.Clear();

                // Sample data - in real app, load from API
                _allShipments.Add(new Shipment
                {
                    Code = "ENV-2025-001",
                    Status = "En Ruta",
                    Route = "San José → Cartago",
                    Driver = "Juan Pérez",
                    CreatedDate = DateTime.Now.AddDays(-2),
                    Weight = 2.5,
                    PackageType = "Paquete mediano"
                });

                _allShipments.Add(new Shipment
                {
                    Code = "ENV-2025-002",
                    Status = "Entregado",
                    Route = "Heredia → Alajuela",
                    Driver = "María López",
                    CreatedDate = DateTime.Now.AddDays(-5),
                    Weight = 1.2,
                    PackageType = "Documento"
                });

                _allShipments.Add(new Shipment
                {
                    Code = "ENV-2025-003",
                    Status = "En Proceso",
                    Route = "Cartago → San José",
                    Driver = "Carlos Ruiz",
                    CreatedDate = DateTime.Now.AddDays(-1),
                    Weight = 5.0,
                    PackageType = "Paquete grande"
                });

                FilterShipments();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al cargar envíos: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void FilterShipments()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Shipments = new ObservableCollection<Shipment>(_allShipments);
            }
            else
            {
                var filtered = _allShipments.Where(s =>
                    s.Code.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    s.Route.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    s.Driver.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

                Shipments = new ObservableCollection<Shipment>(filtered);
            }
        }

        private async Task CreateShipmentAsync()
        {
            await Shell.Current.GoToAsync("createshipment");
        }

        private async Task SelectShipmentAsync(Shipment shipment)
        {
            if (shipment == null) return;

            await Shell.Current.GoToAsync($"shipmentdetails?code={shipment.Code}");
        }
    }
}
