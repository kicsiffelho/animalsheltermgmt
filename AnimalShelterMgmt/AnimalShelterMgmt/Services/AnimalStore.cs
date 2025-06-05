using System.Collections.ObjectModel;
using AnimalShelterMgmt.Models;


namespace AnimalShelterMgmt.Services
{
    public class AnimalStore
    {
        public ObservableCollection<Animal> Animals { get; } = new();

        private static AnimalStore? _instance;
        public static AnimalStore Instance => _instance ??= new AnimalStore();
    }
}