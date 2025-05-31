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

        private void DeleteAnimal_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int animalId)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this animal?",
                    "Confirm Deletion",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    string adminAuth0Id = SessionService.Instance.Auth0UserId;
                    var db = new DatabaseService();
                    bool deleted = db.DeleteAnimal(animalId, adminAuth0Id);

                    if (deleted)
                    {
                        MessageBox.Show("Animal deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        if (this.DataContext is AnimalsViewModel vm)
                        {
                            vm.RefreshAnimals();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Deletion failed. You may not have permission.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

    }
}
