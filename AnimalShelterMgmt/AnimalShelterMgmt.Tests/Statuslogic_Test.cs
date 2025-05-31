using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalShelterMgmt.Models;

namespace AnimalShelterMgmt.Tests
{
    [TestClass]
    public class AnimalAdoptionLogicTests
    {
        private bool CanAdopt(Animal animal)
        {
            return animal.Status == "available";
        }

        [TestMethod]
        public void CanAdopt_ReturnsTrue_WhenStatusIsAvailable()
        {
            var animal = new Animal { Status = "available" };
            Assert.IsTrue(CanAdopt(animal));
        }

        [TestMethod]
        public void CanAdopt_ReturnsFalse_WhenStatusIsAdopted()
        {
            var animal = new Animal { Status = "adopted" };
            Assert.IsFalse(CanAdopt(animal));
        }

        [TestMethod]
        public void CanAdopt_ReturnsFalse_WhenStatusIsFostered()
        {
            var animal = new Animal { Status = "fostered" };
            Assert.IsFalse(CanAdopt(animal));
        }
    }
}
