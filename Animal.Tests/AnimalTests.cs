using FluentAssertions;

namespace Animal.Tests
{
    [TestClass]
    public class AnimalTests
    {
        [TestMethod]
        public void Tiger_NegativeWight_ThrowsNegativeWeightException()
        {
            Action action = () => new Tiger("name", -10.2, "Asia");

            action.Should().Throw<NegativeWeightException>();
        }

        [TestMethod]
        public void Mouse_NegativeWight_ThrowsNegativeWeightException()
        {
            Action action = () => new Mouse("name", -10.2, "Asia");

            action.Should().Throw<NegativeWeightException>();
        }

        [TestMethod]
        public void Zebra_NegativeWight_ThrowsNegativeWeightException()
        {
            Action action = () => new Zebra("name", -10.2, "Asia");

            action.Should().Throw<NegativeWeightException>();
        }

        [TestMethod]
        public void Cat_NegativeWight_ThrowsNegativeWeightException()
        {
            Action action = () => new Cat("name", -10.2, "Asia", "Persian");

            action.Should().Throw<NegativeWeightException>();
        }
    }
}
