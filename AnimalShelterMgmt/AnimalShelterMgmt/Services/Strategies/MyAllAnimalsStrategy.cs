using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalShelterMgmt.Models;


namespace AnimalShelterMgmt.Services.Strategies
{
    public class MyAllAnimalsStrategy : IMyAnimalFilterStrategy
    {
        public IEnumerable<Animal> Filter(IEnumerable<Animal> animals)
        {
            return animals;
        }

        public class CatFilterStrategy : IMyAnimalFilterStrategy
        {
            public IEnumerable<Animal> Filter(IEnumerable<Animal> animals)
            {
                return animals.Where(a => a.Species == "Cat");
            }
        }

        public class DogFilterStrategy : IMyAnimalFilterStrategy
        {
            public IEnumerable<Animal> Filter(IEnumerable<Animal> animals)
            {
                return animals.Where(a => a.Species == "Dog");
            }
        }
    }
}
