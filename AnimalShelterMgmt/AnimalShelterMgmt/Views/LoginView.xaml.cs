using System.Windows.Controls;

namespace AnimalShelterMgmt.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                if (DataContext is ViewModels.LoginViewModel vm)
                {
                    vm.RequestPassword = () => PasswordBox.Password;
                }
            };
        }
    }
}