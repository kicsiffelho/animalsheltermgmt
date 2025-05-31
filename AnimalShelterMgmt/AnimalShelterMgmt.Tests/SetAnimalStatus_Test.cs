using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalShelterMgmt.Models;
using AnimalShelterMgmt.Services;
using System.Linq;

namespace AnimalShelterMgmt.Tests
{
    [TestClass]
    public class DatabaseServiceIntegrationTests
    {
        [TestMethod]
        public void AddAnimal_And_SetStatus_ShouldWork()
        {
            var db = new DatabaseService();

            var animal = new Animal
            {
                Name = "TestDog",
                Species = "Dog",
                Age = 3,
                Description = "Test animal addition",
                ImageUrl = "http://test.hu/dog.jpg"
            };

            var added = db.AddAnimal(animal);
            Assert.IsTrue(added, "Failed to add the animal!");

            var animals = db.GetAnimals();
            var newAnimal = animals.LastOrDefault(a => a.Name == "TestDog" && a.Species == "Dog");
            Assert.IsNotNull(newAnimal, "The animal was not found in the system!");

            string testAuth0Id = "unittestuser";
            db.SetAnimalStatus(newAnimal.Id, testAuth0Id, "owner");

            animals = db.GetAnimals();
            var updatedAnimal = animals.FirstOrDefault(a => a.Id == newAnimal.Id);
            Assert.IsNotNull(updatedAnimal, "The animal was not found after status change!");
            Assert.AreEqual("adopted", updatedAnimal.Status, "Failed to set the status to 'adopted'.");
        }
    }
}
