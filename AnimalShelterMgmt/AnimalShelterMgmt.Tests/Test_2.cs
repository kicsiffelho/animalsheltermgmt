using AnimalShelterMgmt.Services;
using AnimalShelterMgmt.Services.Strategies;
using AnimalShelterMgmt.ViewModels;
using System.Collections.ObjectModel;

namespace AnimalShelterMgmt.Tests;

[TestClass]
public class Test_2
{
    [TestMethod]
    public void AnimalFilterStrategy_FiltersOnlyDogs()
    {
        var animals = new List<Animal>
        {
                new Animal { Name = "Rex", Species = "Dog" },
                new Animal { Name = "Whiskers", Species = "Cat" },
                new Animal { Name = "Buddy", Species = "Dog" }
        };
        var strategy = new MyAllAnimalsStrategy.DogFilterStrategy();
        var result = strategy.Filter(animals).ToList();

        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.All(a => a.Species == "Dog"));
    }

    [TestMethod]
    public void ViewModel_AppliesCatFilter()
    {
        var vm = new MyAnimalsViewModel();
        vm.SetAnimals(new List<Animal>
        {
            new Animal { Species = "Cat" },
            new Animal { Species = "Dog" },
            new Animal { Species = "Cat" }
        });

        vm.SetFilterStrategy(new MyAllAnimalsStrategy.CatFilterStrategy());
        Assert.AreEqual(2, vm.MyAnimals.Count);
        Assert.IsTrue(vm.MyAnimals.All(a => a.Species == "Cat"));
    }

    [TestMethod]
    public void Observer_ReloadsAnimals()
    {
        var vm = new MyAnimalsViewModel();
        var oldAnimals = vm.MyAnimals;

        vm.Update();

        Assert.IsNotNull(vm.MyAnimals);
        Assert.AreNotSame(oldAnimals, vm.MyAnimals);
    }

    [DataTestMethod]
    [DataRow("Dog", typeof(MyAllAnimalsStrategy.DogFilterStrategy))]
    [DataRow("Cat", typeof(MyAllAnimalsStrategy.CatFilterStrategy))]
    public void AnimalFilterStrategy_ReturnsCorrectResults(string species, Type strategyType)
    {
        IMyAnimalFilterStrategy strategy = (IMyAnimalFilterStrategy)Activator.CreateInstance(strategyType)!;

        var animals = new List<Animal>
        {
            new Animal { Species = "Dog" },
            new Animal { Species = "Cat" },
            new Animal { Species = species }
        };
        var result = strategy.Filter(animals).ToList();

        Assert.IsTrue(result.All(a => a.Species == species));
    }

    [TestMethod]
    public void ThrowsException_WhenAnimalListIsNull()
    {
        var strategy = new MyAllAnimalsStrategy.CatFilterStrategy();

        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            strategy.Filter(null);
        });
    }
}
