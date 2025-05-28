using Auth0.OidcClient;
using System.Windows;
using AnimalShelterMgmt.Models;
using AnimalShelterMgmt.ViewModels;
using AnimalShelterMgmt.Services;

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
            var clientOptions = new Auth0ClientOptions
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

            if (!loginResult.IsError)
            {
                // Auth0 azonosító (kulcs az adatbázisban)
                var auth0id = loginResult.User.FindFirst(c => c.Type == "sub")?.Value;

                // Kiegészítő, csak memóriában tárolt adatok (UI megjelenítéshez)
                var username = loginResult.User.FindFirst(c => c.Type == "nickname")?.Value ?? "Vendég";
                var email = loginResult.User.FindFirst(c => c.Type == "email")?.Value ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(auth0id))
                {
                    var db = new DatabaseService();
                    var user = db.SaveOrGetUser(auth0id); // csak az auth0Id-t mentjük
                    user.Username = username; // csak megjelenítéshez
                    user.Email = email;

                    var vm = DataContext as MainWindowViewModel;
                    vm?.SetLoggedInUser(user);
                }
            }
        }


    }
}
