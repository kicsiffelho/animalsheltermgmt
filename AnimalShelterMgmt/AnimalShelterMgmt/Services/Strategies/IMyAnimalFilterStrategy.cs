using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterMgmt.Services.Strategies
{
    public interface IMyAnimalFilterStrategy
    {
        IEnumerable<Animal> Filter (IEnumerable<Animal> animals);
    }
}
