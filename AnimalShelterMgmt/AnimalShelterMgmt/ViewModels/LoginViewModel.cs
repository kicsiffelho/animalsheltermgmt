using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AnimalShelterMgmt.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string errorMessage;

        public ICommand LoginCommand { get; }

        public Func<string>? RequestPassword { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login()
        {
            string password = RequestPassword?.Invoke() ?? "";

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Minden mező kitöltése kötelező!";
                return;
            }

            if (Username == "admin" && password == "1234")
            {
                ErrorMessage = "";
                MessageBox.Show("Sikeres bejelentkezés!");
                // TODO: Átirányítás másik nézetre
            }
            else
            {
                ErrorMessage = "Hibás felhasználónév vagy jelszó!";
            }
        }
    }
}
