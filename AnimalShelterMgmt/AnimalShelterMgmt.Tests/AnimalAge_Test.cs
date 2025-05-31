using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimalShelterMgmt.Models;

namespace AnimalShelterMgmt.Tests
{
    [TestClass]
    public class AnimalAgeValidationTests
    {
        private bool IsValidAge(int age)
        {
            return age >= 0 && age < 100;
        }

        [TestMethod]
        public void IsValidAge_ReturnsTrue_ForPositiveAge()
        {
            Assert.IsTrue(IsValidAge(5));
        }

        [TestMethod]
        public void IsValidAge_ReturnsTrue_ForZero()
        {
            Assert.IsTrue(IsValidAge(0));
        }

        [TestMethod]
        public void IsValidAge_ReturnsFalse_ForNegativeAge()
        {
            Assert.IsFalse(IsValidAge(-1));
        }

        [TestMethod]
        public void IsValidAge_ReturnsFalse_ForUnrealisticallyHighAge()
        {
            Assert.IsFalse(IsValidAge(150));
        }
    }
}
