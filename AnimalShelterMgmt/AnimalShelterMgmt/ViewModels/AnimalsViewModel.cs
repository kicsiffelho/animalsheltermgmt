using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AnimalShelterMgmt.ViewModels
{
    public partial class AnimalsViewModel : ObservableObject
    {
        public ObservableCollection<Animal> Animals { get; } = new ObservableCollection<Animal>();

        public AnimalsViewModel()
        {
            Animals.Add(new Animal
            {
                Name = "Cirmi",
                Species = "Macska",
                Age = 2,
                Description = "Kedves, játékos cica, szereti a simogatást.",
                ImageUrl = "https://placekitten.com/600/400"
            });

            Animals.Add(new Animal
            {
                Name = "Bodri",
                Species = "Kutya",
                Age = 5,
                Description = "Hűséges társ, imád sétálni és játszani.",
                ImageUrl = "https://placedog.net/600/400?id=1"
            });

            Animals.Add(new Animal
            {
                Name = "Tapsi",
                Species = "Nyúl",
                Age = 1,
                Description = "Csendes nyuszi, szereti a friss zöldséget.",
                ImageUrl = "https://placehold.co/600x400?text=Nyúl"
            });
        }
    }

    public class Animal
    {
        public string Name { get; set; } = "";
        public string Species { get; set; } = "";
        public int Age { get; set; }
        public string Description { get; set; } = "";
        public string ImageUrl { get; set; } = ""; // URL vagy fájl elérési út
    }
}
