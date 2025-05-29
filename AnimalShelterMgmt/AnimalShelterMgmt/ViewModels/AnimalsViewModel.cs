using AnimalShelterMgmt.Models;
using AnimalShelterMgmt.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AnimalShelterMgmt.ViewModels
{
    public partial class AnimalsViewModel : ObservableObject
    {
        public ObservableCollection<AnimalItemViewModel> Animals { get; } = new();

        public AnimalsViewModel()
        {
            LoadAnimals();
        }

        public void LoadAnimals()
        {
            Animals.Clear();
            var db = new DatabaseService();
            foreach (var animal in db.GetAnimals())
            {
                Animals.Add(new AnimalItemViewModel(animal));
            }
        }

        public void RefreshAnimals()
        {
            LoadAnimals();
        }
    }
}