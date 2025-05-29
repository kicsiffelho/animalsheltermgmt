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

namespace AnimalShelterMgmt
{
    public partial class MainWindow : Window
    {
        private Auth0Client client;
        public MainWindow()
        {
            InitializeComponent();
            InitializeClient();
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
                }
                UserTextBoxRole.Text = existingUser.Role;
                SessionService.Instance.Auth0UserId = sub;
            }

        }

        private async void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            await client.LogoutAsync();
            UserTextBox.Text = "Guest";
            UserTextBoxRole.Text = "";
            SessionService.Instance.Auth0UserId = "";
        }

    }
}