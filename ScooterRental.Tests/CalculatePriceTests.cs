using FluentAssertions;

namespace ScooterRental.Tests
{
    [TestClass]
    public class CalculatePriceTests
    {
        private CalculateRentPrice _calculateRentPrice;
        private const string DEFAULT_SCOOTER_ID = "1";
        private const decimal DEFAULT_PRICE = 0.1m;

        [TestInitialize]
        public void Setup()
        {
            _calculateRentPrice = new CalculateRentPrice();
        }

        [TestMethod]
        public void CalculatePrice_ShortOneDayRental_ShouldCalculateCorrectPrice()
        {
            var rentalRecord = new RentedScooter(DEFAULT_SCOOTER_ID, new DateTime(2023, 9, 17, 10, 0, 0));
            rentalRecord.RentEnd = new DateTime(2023, 9, 17, 11, 30, 0); 
            var scooter = new Scooter(DEFAULT_SCOOTER_ID, DEFAULT_PRICE);

            decimal price = _calculateRentPrice.CalculatePrice(rentalRecord, scooter);

            price.Should().Be(9.0m);
        }

        [TestMethod]
        public void CalculatePrice_LongOneDayRental_ShouldCalculateCorrectPrice()
        {
            var rentalRecord = new RentedScooter(DEFAULT_SCOOTER_ID, new DateTime(2023, 9, 17, 10, 0, 0));
            rentalRecord.RentEnd = new DateTime(2023, 9, 17, 21, 30, 0);
            var scooter = new Scooter(DEFAULT_SCOOTER_ID, DEFAULT_PRICE);

            decimal price = _calculateRentPrice.CalculatePrice(rentalRecord, scooter);

            price.Should().Be(20.0m);
        }

        [TestMethod]
        public void CalculatePrice_MultiDayRental_ShouldCalculateCorrectPrice()
        {
            var rentalRecord = new RentedScooter(DEFAULT_SCOOTER_ID, new DateTime(2023, 1, 1, 23, 0, 0));
            rentalRecord.RentEnd = new DateTime(2023, 1, 3, 1, 1, 0); 
            var scooter = new Scooter(DEFAULT_SCOOTER_ID, DEFAULT_PRICE);

            decimal price = _calculateRentPrice.CalculatePrice(rentalRecord, scooter);

            price.Should().Be(32.0m);
        }

        [TestMethod]
        public void CalculatePrice_InvalidRentalDates_ShouldThrowException()
        {
            var rentalRecord = new RentedScooter("ScooterId", new DateTime(2023, 9, 17, 10, 0, 0));
            rentalRecord.RentEnd = new DateTime(2023, 9, 16, 15, 30, 0); 
            var scooter = new Scooter("ScooterId", 1.0m);

            Action action = () => _calculateRentPrice.CalculatePrice(rentalRecord, scooter);

            action.Should().Throw<InvalidTimeException>();
        }
    }
}
