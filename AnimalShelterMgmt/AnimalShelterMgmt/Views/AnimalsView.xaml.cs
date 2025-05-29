using AnimalShelterMgmt.Models;
using AnimalShelterMgmt.Services;
using AnimalShelterMgmt.ViewModels;
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
            if (sender is Button button && button.Tag is int animalId)
            {
                string auth0id = SessionService.Instance.Auth0UserId;
                var db = new DatabaseService();

                db.SetAnimalStatus(animalId, auth0id, button.Name == "Foster" ? "foster" : "adopted");

                StatusChangeNotifier.Instance.Notify();

                if (DataContext is AnimalShelterMgmt.ViewModels.AnimalsViewModel vm)
                {
                    vm.RefreshAnimals();
                }

                button.IsEnabled = false;
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