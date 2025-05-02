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

            vm.RequestPassword = () =>
            {
                return (NewPasswordBox.Password, ConfirmPasswordBox.Password);
            };
        }
    }
}
