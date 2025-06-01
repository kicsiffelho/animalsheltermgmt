using AnimalShelterMgmt.Services;
using AnimalShelterMgmt.Models;
using Auth0.OidcClient;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.X509;
using System.Diagnostics;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using AnimalShelterMgmt.ViewModels;
using AnimalShelterMgmt.Views;
using AnimalShelterMgmt.Services.UserRoles;

namespace AnimalShelterMgmt
{
    public partial class MainWindow : Window
    {
        private Auth0Client client;
        public MainWindow()
        {
            InitializeComponent();
            InitializeClient();

            IUserRole guest = new GuestUserRole();
            UserTextBox.Text = guest.GetRoleName();
            ApplyRoleUIVisibility(guest);
        }
        private void InitializeClient()
        {
            Auth0ClientOptions clientOptions = new Auth0ClientOptions
            {
                Domain = "dev-3i6e17z346wb6p1c.us.auth0.com",
                ClientId = "LrO6pwRxzI0GpMa5kvyU9x6FTJFohJkD",
                RedirectUri = "http://localhost:3000",
                Scope = "openid profile email"
            };
            client = new Auth0Client(clientOptions);
            clientOptions.PostLogoutRedirectUri = clientOptions.RedirectUri;
        }

        private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            var loginResult = await client.LoginAsync();

            if (loginResult.IsError == false)
            {
                var user = loginResult.User;
                var nickname = user.FindFirst("nickname")?.Value;
                var sub = user.FindFirst("sub")?.Value;
                UserTextBox.Text = nickname;

                var userService = new UserService();
                var existingUser = userService.GetUserById(sub);
                if (existingUser == null)
                {
                    var newUser = new User
                    {
                        Auth0Id = sub,
                        Role = "Foster",
                        CreatedAt = DateTime.Now
                    };
                    userService.RegisterUser(newUser);
                    existingUser = newUser;
                }

                SessionService.Instance.Auth0UserId = sub;
                SessionService.Instance.CurrentUserRole = existingUser.Role;

                IUserRole userRole = new BaseUserRole(existingUser);
                if (existingUser.Role == "Admin")
                    userRole = new AdminRoleDecorator(userRole);
                else if (existingUser.Role == "Foster")
                    userRole = new FosterRoleDecorator(userRole);
                else if (existingUser.Role == "Owner")
                    userRole = new OwnerRoleDecorator(userRole);
                UserTextBoxRole.Text = userRole.GetRoleName();
                SessionService.Instance.Auth0UserId = sub;

                ApplyRoleUIVisibility(userRole);
            }

        }

        private async void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            await client.LogoutAsync();
            IUserRole guest = new GuestUserRole();
            UserTextBox.Text = guest.GetRoleName();
            UserTextBoxRole.Text = "";
            SessionService.Instance.Auth0UserId = "";
            ApplyRoleUIVisibility(guest);
        }

        private void ApplyRoleUIVisibility(IUserRole role)
        {
            AddNewAnimalBtn.Visibility = role.CanAddAnimal() ? Visibility.Visible : Visibility.Collapsed;
            LogoutBtn.Visibility = role.CanLogout() ? Visibility.Visible : Visibility.Collapsed;
            ShowMyAnimalsBtn.Visibility = role.CanShowMyAnimals() ? Visibility.Visible : Visibility.Collapsed;
            ProfileBtn.Visibility = role.CanShowProfile() ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}