using AnimalShelterMgmt.Models;
using AnimalShelterMgmt.Services;
using Auth0.OidcClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnimalShelterMgmt.Views
{
    public partial class ProfileView : UserControl
    {
        public ProfileView()
        {
            InitializeComponent();

            var vm = new ViewModels.ProfileViewModel();
            DataContext = vm;

            /*vm.RequestPassword = () =>
            {
                return (NewPasswordBox.Password, ConfirmPasswordBox.Password);
            };*/


        }
        private async void SetUserRole_Click(object sender, RoutedEventArgs e)
        {
            string role = ((ComboBoxItem)RoleComboBox.SelectedItem).Content.ToString();
            string auth0id = Auth0Session.UserId;

            var db = new DatabaseService();
            db.SetUserRole(auth0id, role);
        }
    }
}
