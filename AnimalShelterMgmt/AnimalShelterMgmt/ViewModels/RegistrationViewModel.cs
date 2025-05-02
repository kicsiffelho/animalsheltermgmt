using AnimalShelterMgmt.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AnimalShelterMgmt.ViewModels
{
    public partial class RegistrationViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string errorMessage;

        public ICommand RegistrationCommand { get; }

        public Func<(string password, string confirmPassword)>? RequestPasswords { get; set; }
        public RegistrationViewModel()
        {
            RegistrationCommand = new RelayCommand(Registration);
        }

        private void Registration()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Minden mező kötelező!";
                return;
            }

            var (pwd, confirm) = RequestPasswords?.Invoke() ?? ("", "");

            if (string.IsNullOrWhiteSpace(pwd) || pwd != confirm)
            {
                ErrorMessage = "A jelszavak nem egyeznek vagy üresek!";
                return;
            }

            // TODO: Mentés adatbázisba

            ErrorMessage = "";
            MessageBox.Show("Sikeres regisztráció!");
        }
    }
}
