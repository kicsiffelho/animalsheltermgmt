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
                ErrorMessage = "All fields are required!";
                return;
            }

            if (!int.TryParse(Age, out int parsedAge) || parsedAge < 0)
            {
                ErrorMessage = "Age must be a positive integer!";
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
                MessageBox.Show("Animal successfully added to the database!");

                Name = Species = Age = ImageUrl = Description = "";
            }
            else
            {
                ErrorMessage = "An error occurred while saving.";
            }
        }

        private void SubmitStatusChange()
        {
            ErrorMessage = "";

            if (SelectedAnimalId <= 0 || string.IsNullOrWhiteSpace(SelectedRelationType))
            {
                ErrorMessage = "Please select an animal and a relation type!";
                return;
            }

            string auth0id = SessionService.Instance.Auth0UserId;

            AnimalStatusChangeTemplate action = SelectedRelationType switch
            {
                "owner" => new AdoptAnimal(),
                "foster" => new FosterAnimal(),
                _ => throw new InvalidOperationException("Unknown relation type.")
            };

            try
            {
                action.ChangeStatus(SelectedAnimalId, auth0id);
                MessageBox.Show("Status updated successfully!");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Error setting status: " + ex.Message;
            }
        }
    }
}
