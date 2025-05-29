using AnimalShelterMgmt.Models;
using AnimalShelterMgmt.Services;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AnimalShelterMgmt.Views
{
    public partial class AnimalsView : UserControl
    {
        public AnimalsView()
        {
            InitializeComponent();
        }

        private async void SetAnimalStatus_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int animalId = (int)button.Tag;
            var userService = new UserService();
            string auth0id = SessionService.Instance.Auth0UserId;
            var db = new DatabaseService();
            db.SetAnimalStatus(animalId, auth0id, button.Name);
        }
    }
}