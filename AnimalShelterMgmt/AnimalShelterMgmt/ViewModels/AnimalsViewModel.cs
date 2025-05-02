using AnimalShelterMgmt.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using AnimalShelterMgmt.Models;

namespace AnimalShelterMgmt.ViewModels
{
    public partial class AnimalsViewModel : ObservableObject
    {
        public ObservableCollection<Animal> Animals => AnimalStore.Instance.Animals;

        public AnimalsViewModel()
        {
            if (Animals.Count == 0)
            {
                var db = new DatabaseService();
                foreach (var a in db.GetAnimals())
                    Animals.Add(a);
            }
        }

    }
}

public class Animal
    {
        public string Name { get; set; } = "";
        public string Species { get; set; } = "";
        public int Age { get; set; }
        public string Description { get; set; } = "";
        public string ImageUrl { get; set; } = "";
    }
