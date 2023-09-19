using Animal;
using FluentAssertions;

namespace Animal.Tests
{
    [TestClass]
    public class VegetableTests
    {
        [TestMethod]
        public void Vegetable_AddVegetable_ReturnsQuantityCorrectly()
        {
            Vegetable meat = new Vegetable(12.0);

            meat.Quantity.Should().Be(12.0);
        }

        [TestMethod]
        public void Vegetable_AddNegativeQuantity_ThrowsNegativeExeption()
        {
            Action action = () => new Vegetable(-12.0);

            action.Should().Throw<NegativeQuantityExeption>();
        }
    }
}