using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReportesDePaqueteria.MVVM.Models;

namespace ReportesDePaqueteria.MVVM.ViewModels
{
    public class CreateShipmentViewModel : BaseViewModel
    {
        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _packageType;
        public string PackageType
        {
            get => _packageType;
            set => SetProperty(ref _packageType, value);
        }

        private string _weight;
        public string Weight
        {
            get => _weight;
            set => SetProperty(ref _weight, value);
        }

        // Origin Address
        private string _originStreet;
        public string OriginStreet
        {
            get => _originStreet;
            set => SetProperty(ref _originStreet, value);
        }

        private string _originCity;
        public string OriginCity
        {
            get => _originCity;
            set => SetProperty(ref _originCity, value);
        }

        private string _originPostalCode;
        public string OriginPostalCode
        {
            get => _originPostalCode;
            set => SetProperty(ref _originPostalCode, value);
        }

        // Destination Address
        private string _destinationStreet;
        public string DestinationStreet
        {
            get => _destinationStreet;
            set => SetProperty(ref _destinationStreet, value);
        }

        private string _destinationCity;
        public string DestinationCity
        {
            get => _destinationCity;
            set => SetProperty(ref _destinationCity, value);
        }

        private string _destinationPostalCode;
        public string DestinationPostalCode
        {
            get => _destinationPostalCode;
            set => SetProperty(ref _destinationPostalCode, value);
        }

        private string _recipientName;
        public string RecipientName
        {
            get => _recipientName;
            set => SetProperty(ref _recipientName, value);
        }

        private string _recipientPhone;
        public string RecipientPhone
        {
            get => _recipientPhone;
            set => SetProperty(ref _recipientPhone, value);
        }

        public List<string> PackageTypes { get; } = new List<string>
        {
            "Documento",
            "Paquete pequeño",
            "Paquete mediano",
            "Paquete grande"
        };

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public CreateShipmentViewModel()
        {
            Title = "Registrar Nuevo Envío";
            SaveCommand = new Command(async () => await SaveShipmentAsync());
            CancelCommand = new Command(async () => await CancelAsync());
        }

        private async Task SaveShipmentAsync()
        {
            // Validate fields
            if (string.IsNullOrWhiteSpace(Description) ||
                string.IsNullOrWhiteSpace(PackageType) ||
                string.IsNullOrWhiteSpace(Weight) ||
                string.IsNullOrWhiteSpace(OriginStreet) ||
                string.IsNullOrWhiteSpace(OriginCity) ||
                string.IsNullOrWhiteSpace(DestinationStreet) ||
                string.IsNullOrWhiteSpace(DestinationCity) ||
                string.IsNullOrWhiteSpace(RecipientName) ||
                string.IsNullOrWhiteSpace(RecipientPhone))
            {
                await Shell.Current.DisplayAlert("Error",
                    "Por favor complete todos los campos requeridos", "OK");
                return;
            }

            if (!double.TryParse(Weight, out double weightValue) || weightValue <= 0)
            {
                await Shell.Current.DisplayAlert("Error",
                    "Por favor ingrese un peso válido", "OK");
                return;
            }

            if (IsBusy) return;

            IsBusy = true;

            try
            {
                // Simulate API call
                await Task.Delay(1500);

                var newShipment = new Shipment
                {
                    Code = $"ENV-2025-{Random.Shared.Next(100, 999)}",
                    Description = Description,
                    PackageType = PackageType,
                    Weight = weightValue,
                    Status = "Registrado",
                    CreatedDate = DateTime.Now,
                    Origin = new Address
                    {
                        Street = OriginStreet,
                        City = OriginCity,
                        PostalCode = OriginPostalCode
                    },
                    Destination = new Address
                    {
                        Street = DestinationStreet,
                        City = DestinationCity,
                        PostalCode = DestinationPostalCode
                    },
                    RecipientName = RecipientName,
                    RecipientPhone = RecipientPhone,
                    Route = $"{OriginCity} → {DestinationCity}"
                };

                await Shell.Current.DisplayAlert("Éxito",
                    $"Envío registrado correctamente\nCódigo: {newShipment.Code}", "OK");

                // Navigate back
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error",
                    $"Error al registrar envío: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CancelAsync()
        {
            var confirm = await Shell.Current.DisplayAlert("Cancelar",
                "¿Está seguro que desea cancelar el registro?", "Sí", "No");

            if (confirm)
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
