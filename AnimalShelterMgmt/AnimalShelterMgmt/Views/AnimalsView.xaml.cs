using AnimalShelterMgmt.Models;
using AnimalShelterMgmt.Services;
using AnimalShelterMgmt.Services.Template;
using AnimalShelterMgmt.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AnimalShelterMgmt.Views
{
    public partial class AnimalsView : UserControl
    {
        public AnimalsView()
        {
            InitializeComponent();
        }

        private void SetAnimalStatus_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int animalId)
            {
                string auth0id = SessionService.Instance.Auth0UserId;
                var db = new DatabaseService();

                int? userId = db.GetUserIdByAuth0Id(auth0id);

                if (userId == null)
                {
                    MessageBox.Show("No user found in the database.");
                    return;
                }

                AnimalStatusChangeTemplate action = button.Name switch
                {
                    "Foster" => new FosterAnimal(),
                    "Owner" => new AdoptAnimal(),
                    _ => throw new InvalidOperationException("Unknown operation.")
                };

                try
                {
                    action.ChangeStatus(animalId, auth0id);

                    StatusChangeNotifier.Instance.Notify();

                    if (DataContext is AnimalsViewModel vm)
                    {
                        vm.RefreshAnimals();
                    }

                    button.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is AnimalsViewModel vm)
            {
                vm.RefreshAnimals();
            }
        }
    }
}
