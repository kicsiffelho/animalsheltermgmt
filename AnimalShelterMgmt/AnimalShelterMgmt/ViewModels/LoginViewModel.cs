using AnimalShelterMgmt.Helpers;
using AnimalShelterMgmt.Services;
using AnimalShelterMgmt.Stores;
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
        public event Action? LoginSucceeded;

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

            var userService = new UserService();
            var user = userService.GetUserByUsername(Username);

            if (user == null)
            {
                ErrorMessage = "Nincs ilyen felhasználó.";
                return;
            }

            var hashedInput = PasswordHelper.Hash(password);

            if (user.PasswordHash != hashedInput)
            {
                ErrorMessage = "Hibás jelszó.";
                return;
            }

            ErrorMessage = "";
            UserStore.Instance.CurrentUser = user;
            MessageBox.Show($"Sikeres bejelentkezés! Üdv, {user.Username} ({user.Role})");

            LoginSucceeded?.Invoke();
        }
    }
}
