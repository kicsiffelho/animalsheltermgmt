using AnimalShelterMgmt.Services.Proxy;
using System.Collections.Generic;
using System.Diagnostics;

namespace AnimalShelterMgmt.Services.Proxy
{
    public class AnimalImageProxy : IAnimalImageProvider
    {
        private readonly IAnimalImageProvider _realService;
        private readonly Dictionary<int, string> _cache = new();

        public AnimalImageProxy(IAnimalImageProvider realService)
        {
            _realService = realService;
        }

        public string GetImageUrl(int animalId)
        {
            Debug.WriteLine($"GetImageUrl called for AnimalId: {animalId}");
            var animal = AnimalStore.Instance.Animals.FirstOrDefault(a => a.Id == animalId);
            var url = animal?.ImageUrl ?? "nincs-kep.png";
            Debug.WriteLine($"Returning URL: {url}");
            return url;
        }
    }
}
