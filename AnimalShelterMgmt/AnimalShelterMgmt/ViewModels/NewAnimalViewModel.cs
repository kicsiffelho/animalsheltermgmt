using AnimalShelterMgmt.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using AnimalShelterMgmt.Models;
using AnimalShelterMgmt.Services.Template;

namespace AnimalShelterMgmt.ViewModels
{
    public partial class NewAnimalViewModel : ObservableObject
    {
        [ObservableProperty] private string name = "";
        [ObservableProperty] private string species = "";
        [ObservableProperty] private string age = "";
        [ObservableProperty] private string imageUrl = "";
        [ObservableProperty] private string description = "";
        [ObservableProperty] private string errorMessage = "";

        public ICommand AddAnimalCommand { get; }

        [ObservableProperty] private string selectedRelationType;
        [ObservableProperty] private int selectedAnimalId;
        [ObservableProperty] private int selectedUserId;

        public ICommand SubmitStatusChangeCommand { get; }

        public NewAnimalViewModel()
        {
            AddAnimalCommand = new RelayCommand(AddAnimal);
            SubmitStatusChangeCommand = new RelayCommand(SubmitStatusChange);
        }

        private void AddAnimal()
        {
            ErrorMessage = "";

            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Species) ||
                string.IsNullOrWhiteSpace(Age) ||
                string.IsNullOrWhiteSpace(ImageUrl))
            {
                ErrorMessage = "Minden mező kitöltése kötelező!";
                return;
            }

            if (!int.TryParse(Age, out int parsedAge) || parsedAge < 0)
            {
                ErrorMessage = "Az életkor csak pozitív egész szám lehet!";
                return;
            }

            var newAnimal = new Animal
            {
                Name = Name,
                Species = Species,
                Age = parsedAge,
                Description = Description,
                ImageUrl = ImageUrl
            };

            var db = new DatabaseService();
            bool success = db.AddAnimal(newAnimal);

            if (success)
            {
                AnimalStore.Instance.Animals.Add(newAnimal);
                MessageBox.Show("Állat sikeresen hozzáadva az adatbázishoz!");

                Name = Species = Age = ImageUrl = Description = "";
            }
            else
            {
                ErrorMessage = "Hiba történt a mentés során.";
            }
        }

        private void SubmitStatusChange()
        {
            ErrorMessage = "";

            if (SelectedAnimalId <= 0 || string.IsNullOrWhiteSpace(SelectedRelationType))
            {
                ErrorMessage = "Kérlek válassz állatot és kapcsolat típust!";
                return;
            }

            string auth0id = SessionService.Instance.Auth0UserId;

            AnimalStatusChangeTemplate action = SelectedRelationType switch
            {
                "owner" => new AdoptAnimal(),
                "foster" => new FosterAnimal(),
                _ => throw new InvalidOperationException("Ismeretlen kapcsolat típus.")
            };

            try
            {
                action.ChangeStatus(SelectedAnimalId, auth0id);
                MessageBox.Show("Státusz sikeresen frissítve!");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Hiba a státusz beállításakor: " + ex.Message;
            }
        }
    }
}
