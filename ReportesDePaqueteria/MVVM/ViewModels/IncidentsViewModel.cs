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
    public class IncidentsViewModel : BaseViewModel
    {
        private ObservableCollection<Incident> _incidents;
        public ObservableCollection<Incident> Incidents
        {
            get => _incidents;
            set => SetProperty(ref _incidents, value);
        }

        private string _selectedStatus = "Todos";
        public string SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                SetProperty(ref _selectedStatus, value);
                FilterIncidents();
            }
        }

        private string _selectedCategory = "Todas";
        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                SetProperty(ref _selectedCategory, value);
                FilterIncidents();
            }
        }

        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                SetProperty(ref _selectedDate, value);
                FilterIncidents();
            }
        }

        public List<string> StatusOptions { get; } = new List<string>
        {
            "Todos", "Abierto", "En Proceso", "Resuelto", "Cerrado"
        };

        public List<string> CategoryOptions { get; } = new List<string>
        {
            "Todas", "Retraso", "Daño", "Extravío", "Error en dirección", "Otro"
        };

        public ICommand RefreshCommand { get; }
        public ICommand CreateIncidentCommand { get; }
        public ICommand SelectIncidentCommand { get; }
        public ICommand FilterCommand { get; }

        private ObservableCollection<Incident> _allIncidents;

        public IncidentsViewModel()
        {
            Title = "Incidentes";
            Incidents = new ObservableCollection<Incident>();
            _allIncidents = new ObservableCollection<Incident>();

            RefreshCommand = new Command(async () => await LoadIncidentsAsync());
            CreateIncidentCommand = new Command(async () => await CreateIncidentAsync());
            SelectIncidentCommand = new Command<Incident>(async (incident) => await SelectIncidentAsync(incident));
            FilterCommand = new Command(() => FilterIncidents());

            LoadIncidentsAsync();
        }

        private async Task LoadIncidentsAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                await Task.Delay(500); // Simulate loading

                _allIncidents.Clear();

                // Sample data
                _allIncidents.Add(new Incident
                {
                    Code = "INC-2025-047",
                    Title = "Paquete dañado durante transporte",
                    Priority = "Alta",
                    Status = "En Proceso",
                    Date = DateTime.Now.AddHours(-2),
                    Category = "Daño",
                    ShipmentCode = "ENV-2025-045",
                    Description = "El paquete llegó con daños visibles en la esquina superior."
                });

                _allIncidents.Add(new Incident
                {
                    Code = "INC-2025-048",
                    Title = "Retraso en entrega",
                    Priority = "Media",
                    Status = "Abierto",
                    Date = DateTime.Now.AddHours(-1),
                    Category = "Retraso",
                    ShipmentCode = "ENV-2025-001",
                    Description = "El cliente reporta que el paquete no ha llegado en la fecha estimada."
                });

                _allIncidents.Add(new Incident
                {
                    Code = "INC-2025-046",
                    Title = "Dirección incorrecta",
                    Priority = "Baja",
                    Status = "Resuelto",
                    Date = DateTime.Now.AddDays(-2),
                    Category = "Error en dirección",
                    ShipmentCode = "ENV-2025-040",
                    Description = "La dirección proporcionada no coincidía con la ubicación real."
                });

                FilterIncidents();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al cargar incidentes: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void FilterIncidents()
        {
            var filtered = _allIncidents.AsEnumerable();

            if (SelectedStatus != "Todos")
            {
                filtered = filtered.Where(i => i.Status == SelectedStatus);
            }

            if (SelectedCategory != "Todas")
            {
                filtered = filtered.Where(i => i.Category == SelectedCategory);
            }

            // Filter by date (same day)
            filtered = filtered.Where(i => i.Date.Date <= SelectedDate.Date);

            Incidents = new ObservableCollection<Incident>(filtered.OrderByDescending(i => i.Date));
        }

        private async Task CreateIncidentAsync()
        {
            await Shell.Current.GoToAsync("createincident");
        }

        private async Task SelectIncidentAsync(Incident incident)
        {
            if (incident == null) return;

            await Shell.Current.GoToAsync($"incidentdetails?code={incident.Code}");
        }
    }
}
