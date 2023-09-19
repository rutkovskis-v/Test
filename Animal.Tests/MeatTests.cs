using Animal;
using FluentAssertions;

namespace Animal.Tests
{
    [TestClass]
    public class MeatTests
    {
        [TestMethod]
        public void Meat_AddMeat_ReturnsQuantityCorrectly()
        {
            Meat meat = new Meat(12.0);

            meat.Quantity.Should().Be(12.0);
        }

        [TestMethod]
        public void Meat_AddNegativeQuantity_ThrowsNegativeExeption()
        {
            Action action = () => new Meat(-12.0);

            action.Should().Throw<NegativeQuantityExeption>();
        }
    }
}