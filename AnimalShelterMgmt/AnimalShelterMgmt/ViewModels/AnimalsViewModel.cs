using AnimalShelterMgmt.Models;
using AnimalShelterMgmt.Services;
using AnimalShelterMgmt.Services.Proxy;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AnimalShelterMgmt.ViewModels
{
    public partial class AnimalsViewModel : ObservableObject
    {
        private readonly IAnimalImageProvider _imageProvider;

        public ObservableCollection<Animal> Animals { get; } = new();

        public AnimalsViewModel()
        {
            _imageProvider = new AnimalImageProxy(new AnimalImageService());
            LoadAnimals();
        }

        public void LoadAnimals()
        {
            var db = new DatabaseService();
            var animalsFromDb = db.GetAnimals();

            AnimalStore.Instance.Animals.Clear();
            foreach (var animal in animalsFromDb)
            {
                AnimalStore.Instance.Animals.Add(animal);
            }

            Animals.Clear();
            foreach (var animal in animalsFromDb)
            {
                animal.ImageUrl = _imageProvider.GetImageUrl(animal.Id);
                Animals.Add(animal);
            }
        }


        public void RefreshAnimals()
        {
            LoadAnimals();
        }
    }
}
