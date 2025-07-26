using ReportesDePaqueteria.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReportesDePaqueteria.MVVM.ViewModels
{
    public class TrackingViewModel : BaseViewModel
    {
        private string _trackingCode;
        public string TrackingCode
        {
            get => _trackingCode;
            set => SetProperty(ref _trackingCode, value);
        }

        private TrackingInfo _trackingInfo;
        public TrackingInfo TrackingInfo
        {
            get => _trackingInfo;
            set => SetProperty(ref _trackingInfo, value);
        }

        private bool _showResult;
        public bool ShowResult
        {
            get => _showResult;
            set => SetProperty(ref _showResult, value);
        }

        public ICommand SearchCommand { get; }
        public ICommand ClearCommand { get; }

        public TrackingViewModel()
        {
            Title = "Rastrear Envío";
            SearchCommand = new Command(async () => await SearchShipmentAsync());
            ClearCommand = new Command(Clear);
        }

        private async Task SearchShipmentAsync()
        {
            if (string.IsNullOrWhiteSpace(TrackingCode))
            {
                await Shell.Current.DisplayAlert("Error", "Por favor ingrese un código de envío", "OK");
                return;
            }

            if (IsBusy) return;

            IsBusy = true;
            ShowResult = false;

            try
            {
                await Task.Delay(1000); // Simulate API call

                // Simulate tracking lookup
                if (TrackingCode.ToUpper() == "ENV-2025-001")
                {
                    TrackingInfo = new TrackingInfo
                    {
                        ShipmentCode = "ENV-2025-001",
                        Events = new List<TrackingEvent>
                        {
                            new TrackingEvent
                            {
                                DateTime = DateTime.Now.AddDays(-2),
                                Status = "Registrado",
                                Description = "Paquete registrado en el sistema",
                                Location = "San José - Centro de Distribución",
                                IsCompleted = true
                            },
                            new TrackingEvent
                            {
                                DateTime = DateTime.Now.AddDays(-2).AddHours(4),
                                Status = "En Proceso",
                                Description = "Paquete en preparación para envío",
                                Location = "San José - Centro de Distribución",
                                IsCompleted = true
                            },
                            new TrackingEvent
                            {
                                DateTime = DateTime.Now.AddDays(-1),
                                Status = "En Ruta",
                                Description = "Paquete en tránsito hacia destino",
                                Location = "Ruta San José - Cartago",
                                DriverName = "Juan Pérez",
                                IsCompleted = false
                            },
                            new TrackingEvent
                            {
                                DateTime = DateTime.Now.AddDays(1),
                                Status = "Entregado",
                                Description = "Entrega programada",
                                Location = "Cartago - Destino final",
                                IsCompleted = false
                            }
                        }
                    };
                    ShowResult = true;
                }
                else
                {
                    await Shell.Current.DisplayAlert("No encontrado",
                        "No se encontró el envío con ese código", "OK");
                    TrackingInfo = null;
                    ShowResult = false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error",
                    $"Error al buscar envío: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void Clear()
        {
            TrackingCode = string.Empty;
            TrackingInfo = null;
            ShowResult = false;
        }
    }
}
