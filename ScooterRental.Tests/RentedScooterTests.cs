using FluentAssertions;

namespace ScooterRental.Tests
{
    [TestClass]
    public class RentedScooterTests
    {
        [TestMethod]
        public void RentStart_ShouldReturnExpectedValue()
        {
            DateTime startTime = DateTime.Parse("2000-01-01 10:30:00");
            string scooterId = "1";

            var rentedScooter = new RentedScooter(scooterId, startTime);

            rentedScooter.RentStart.Should().Be(startTime);
        }
    }
}
