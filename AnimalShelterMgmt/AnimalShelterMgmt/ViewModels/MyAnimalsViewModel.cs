using AnimalShelterMgmt.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using AnimalShelterMgmt.Models;
using System.Diagnostics;
using Auth0.OidcClient;
using MySqlX.XDevAPI;

namespace AnimalShelterMgmt.ViewModels
{
    public partial class MyAnimalsViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Animal> myAnimals;

        public MyAnimalsViewModel()
        {
            LoadAnimals();
        }

        private void LoadAnimals()
        {
            string auth0id = SessionService.Instance.Auth0UserId;
            var db = new DatabaseService();
            var animals = db.GetAnimalsByUser(auth0id);
            MyAnimals = new ObservableCollection<Animal>(animals);
        }

    }
}