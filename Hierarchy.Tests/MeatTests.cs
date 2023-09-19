using Hierarchy;
using FluentAssertions;

namespace Hierarchy.Tests
{
    [TestClass]
    public class MeatTests
    {

        [TestMethod]
        public void Meat_Quantity_returnsCorrectQuantity()
        {
            double expectedQuantity = 10.0;

            Meat meat = new Meat(expectedQuantity);

            Assert.AreEqual(expectedQuantity, meat.Quantity);
        }

        [TestMethod]
        public void Meat_InheritsFromFood()
        {
            Meat meat = new Meat(5.0);

            Assert.IsInstanceOfType(meat, typeof(Food));
        }
    }
}
