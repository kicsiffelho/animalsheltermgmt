using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalShelterMgmt.Models;
using AnimalShelterMgmt.Services;
using System.Linq;

[TestClass]
public class AnimalListTests
{
    [TestMethod]
    public void GetAnimals_ShouldContain_RecentlyAddedAnimal()
    {
        var db = new DatabaseService();

        var animal = new Animal
        {
            Name = "TestListDog",
            Species = "TestDog",
            Age = 5,
            Description = "Unit test for listing animals",
            ImageUrl = "http://test.hu/test.jpg"
        };

        db.AddAnimal(animal);

        var animals = db.GetAnimals();
        var found = animals.LastOrDefault(a => a.Name == "TestListDog" && a.Species == "TestDog");

        Assert.IsNotNull(found, "The added animal was not found in the list.");
    }
}
