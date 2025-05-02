using AnimalShelterMgmt.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using AnimalShelterMgmt.Models;

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

        public NewAnimalViewModel()
        {
            AddAnimalCommand = new RelayCommand(AddAnimal);
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
                MessageBox.Show("Állat sikeresen hozzáadva az adatbázishoz!");

                Name = Species = Age = ImageUrl = Description = "";
            }
            else
            {
                ErrorMessage = "Hiba történt a mentés során.";
            }

            if (success)
            {
                AnimalStore.Instance.Animals.Add(newAnimal);

                MessageBox.Show("Állat sikeresen hozzáadva az adatbázishoz!");
                Name = Species = Age = ImageUrl = Description = "";
            }
        }
    }
}
