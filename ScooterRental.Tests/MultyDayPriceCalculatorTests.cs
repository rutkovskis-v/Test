using FluentAssertions;

namespace ScooterRental.Tests
{
    [TestClass]
    public class MultyDayPriceCalculatorTests
    {
        private MultyDayPriceCalculator _priceCalculator;
        private const string DEFAULT_SCOOTER_ID = "1";
        private const decimal DEFAULT_PRICE = 0.1m;

        [TestInitialize]
        public void Setup()
        {
            _priceCalculator = new MultyDayPriceCalculator();
        }

        [TestMethod]
        public void CalculateDailyPrice_SingleDayRental_ShouldCalculateCorrectly()
        {
            var rentalRecord = new RentedScooter(DEFAULT_SCOOTER_ID, new DateTime(2023, 9, 1, 12, 0, 0));
            var scooter = new Scooter(DEFAULT_SCOOTER_ID, DEFAULT_PRICE);
            var rentEnd = new DateTime(2023, 9, 1, 12, 15, 0);

            var result = _priceCalculator.CalculateDailyPrice(rentalRecord, scooter, rentEnd);

            result.Should().Be(1.50m);
        }

        [TestMethod]
        public void CalculateDailyPrice_MultipleDaysRental_ShouldCalculateCorrectly()
        {
            var rentalRecord = new RentedScooter(DEFAULT_SCOOTER_ID, new DateTime(2023, 9, 1, 12, 0, 0));
            var scooter = new Scooter(DEFAULT_SCOOTER_ID, DEFAULT_PRICE);
            var rentEnd = new DateTime(2023, 9, 3, 15, 30, 0);

            var result = _priceCalculator.CalculateDailyPrice(rentalRecord, scooter, rentEnd);

            result.Should().Be(60.00m);
        }

        [TestMethod]
        public void CalculateDailyPrice_SingleDayRental_MaximumPrice_ShouldCalculateCorrectly()
        {
            var rentalRecord = new RentedScooter(DEFAULT_SCOOTER_ID, new DateTime(2023, 9, 1, 12, 0, 0));
            var scooter = new Scooter(DEFAULT_SCOOTER_ID, 5.0m); 
            var rentEnd = new DateTime(2023, 9, 1, 15, 30, 0);

            var result = _priceCalculator.CalculateDailyPrice(rentalRecord, scooter, rentEnd);

            result.Should().Be(20.00m); 
        }
    }
}
