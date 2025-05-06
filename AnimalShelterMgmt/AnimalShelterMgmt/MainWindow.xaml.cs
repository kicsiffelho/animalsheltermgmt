using Auth0.OidcClient;
using MySqlX.XDevAPI;
using System.Diagnostics;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
                var nickname = user.FindFirst(c => c.Type == "nickname")?.Value;
                var sid = user.FindFirst(c => c.Type == "sid")?.Value;
                UserTextBox.Text = nickname;
            }
        }

        private async void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            await client.LogoutAsync();
            UserTextBox.Text = "Vendég";
        }

    }
}