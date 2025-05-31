using AnimalShelterMgmt.Models;
using AnimalShelterMgmt.Services.Proxy;
using System.Linq;

namespace AnimalShelterMgmt.Services.Proxy
{
    public class AnimalImageService : IAnimalImageProvider
    {
        public string GetImageUrl(int animalId)
        {
            var animal = AnimalStore.Instance.Animals.FirstOrDefault(a => a.Id == animalId);
            return animal?.ImageUrl ?? "nincs-kep.png";
        }
    }
}
