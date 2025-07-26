using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Storage;
using System.Text.Json;
using ReportesDePaqueteria.MVVM.Models;

namespace ReportesDePaqueteria.MVVM.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        private int _totalShipments;
        public int TotalShipments
        {
            get => _totalShipments;
            set => SetProperty(ref _totalShipments, value);
        }

        private int _activeIncidents;
        public int ActiveIncidents
        {
            get => _activeIncidents;
            set => SetProperty(ref _activeIncidents, value);
        }

        public ICommand MyShipmentsCommand { get; }
        public ICommand IncidentHistoryCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand EditProfileCommand { get; }

        public ProfileViewModel()
        {
            Title = "Mi Perfil";

            MyShipmentsCommand = new Command(async () => await NavigateToMyShipments());
            IncidentHistoryCommand = new Command(async () => await NavigateToIncidentHistory());
            SettingsCommand = new Command(async () => await NavigateToSettings());
            LogoutCommand = new Command(async () => await LogoutAsync());
            EditProfileCommand = new Command(async () => await EditProfileAsync());

            LoadUserData();
        }

        private void LoadUserData()
        {
            // Get current user from storage (in real app, use secure storage)
            if (Application.Current.Properties.ContainsKey("CurrentUser"))
            {
                CurrentUser = Application.Current.Properties["CurrentUser"] as User;
            }
            else
            {
                // Default user for demo
                CurrentUser = new User
                {
                    Id = "2",
                    Name = "Juan Cliente",
                    Email = "cliente@ejemplo.com",
                    Role = "Cliente",
                    AvatarUrl = "user_avatar.png"
                };
            }

            // Load statistics (simulated)
            TotalShipments = 23;
            ActiveIncidents = 2;
        }

        private async Task NavigateToMyShipments()
        {
            // Navigate to shipments tab
            await Shell.Current.GoToAsync("//main/shipments");
        }

        private async Task NavigateToIncidentHistory()
        {
            await Shell.Current.GoToAsync("incidenthistory");
        }

        private async Task NavigateToSettings()
        {
            await Shell.Current.DisplayAlert("Configuración",
                "Configuración disponible próximamente", "OK");
        }

        private async Task EditProfileAsync()
        {
            await Shell.Current.DisplayAlert("Editar Perfil",
                "Edición de perfil disponible próximamente", "OK");
        }

        private async Task LogoutAsync()
        {
            var confirm = await Shell.Current.DisplayAlert("Cerrar Sesión",
                "¿Está seguro que desea cerrar sesión?", "Sí", "No");

            if (confirm)
            {
                // Clear user data
                Application.Current.Properties.Remove("CurrentUser");

                // Navigate to login
                await Shell.Current.GoToAsync("//login");
            }
        }
    }
}
