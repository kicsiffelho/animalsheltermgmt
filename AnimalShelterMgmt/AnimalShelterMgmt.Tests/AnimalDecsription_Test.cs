using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnimalShelterMgmt.Tests
{
    [TestClass]
    public class AnimalDescriptionValidationTests
    {
        private bool IsValidDescription(string description)
        {
            if (string.IsNullOrEmpty(description)) return false;
            return description.Length >= 3 && description.Length <= 500;
        }

        [TestMethod]
        public void IsValidDescription_ReturnsFalse_WhenDescriptionIsNull()
        {
            Assert.IsFalse(IsValidDescription(null));
        }

        [TestMethod]
        public void IsValidDescription_ReturnsFalse_WhenDescriptionIsTooShort()
        {
            Assert.IsFalse(IsValidDescription("hi"));
        }

        [TestMethod]
        public void IsValidDescription_ReturnsFalse_WhenDescriptionIsTooLong()
        {
            string longText = new string('x', 600);
            Assert.IsFalse(IsValidDescription(longText));
        }

        [TestMethod]
        public void IsValidDescription_ReturnsTrue_WhenDescriptionIsInValidRange()
        {
            Assert.IsTrue(IsValidDescription("This is a good dog."));
        }
    }
}
