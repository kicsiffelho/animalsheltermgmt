using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AnimalShelterMgmt.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string email;

        public ICommand SaveProfileCommand { get; }

        public Func<(string newPassword, string confirmPassword)>? RequestPassword { get; set; }

        public ProfileViewModel()
        {
            Username = "admin_user";
            Email = "admin@example.com";

            SaveProfileCommand = new RelayCommand(SaveProfile);
        }

        private void SaveProfile()
        {
            var (pwd, confirm) = RequestPassword?.Invoke() ?? ("", "");

            if (string.IsNullOrWhiteSpace(pwd) || pwd != confirm)
            {
                MessageBox.Show("A jelszavak nem egyeznek vagy üresek.");
                return;
            }

            MessageBox.Show("Profil mentve.");
        }
    }
}