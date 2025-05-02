using System.Windows.Controls;

namespace AnimalShelterMgmt.Views
{
    public partial class RegistrationView : UserControl
    {
        public RegistrationView()
        {
            InitializeComponent();

            if (DataContext is ViewModels.RegistrationViewModel vm)
            {
                vm.RequestPasswords = () =>
                {
                    return (PasswordBox.Password, ConfirmBox.Password);
                };
            }
        }
    }
}
