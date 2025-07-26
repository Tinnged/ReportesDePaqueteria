using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReportesDePaqueteria.MVVM.Models;

namespace ReportesDePaqueteria.MVVM.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {
            Title = "Iniciar Sesión";
            LoginCommand = new Command(async () => await LoginAsync());
            RegisterCommand = new Command(async () => await RegisterAsync());
        }

        private async Task LoginAsync()
        {
            if (IsBusy) return;

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Error", "Por favor complete todos los campos", "OK");
                return;
            }

            IsBusy = true;

            try
            {
                // Simulate authentication
                await Task.Delay(1000);

                User user = null;
                if (Email == "admin@ejemplo.com" && Password == "admin123")
                {
                    user = new User { Id = "1", Name = "Admin Usuario", Email = Email, Role = "Admin" };
                }
                else if (Email == "cliente@ejemplo.com" && Password == "cliente123")
                {
                    user = new User { Id = "2", Name = "Juan Cliente", Email = Email, Role = "Cliente" };
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Credenciales incorrectas", "OK");
                    return;
                }

                // Store user info (in a real app, use secure storage)
                Application.Current.Properties["CurrentUser"] = user;

                // Navigate to main app
                await Shell.Current.GoToAsync("//main");

                // Clear fields
                Email = string.Empty;
                Password = string.Empty;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Error al iniciar sesión: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RegisterAsync()
        {
            await Shell.Current.DisplayAlert("Registro", "Funcionalidad de registro próximamente", "OK");
        }
    }
}
