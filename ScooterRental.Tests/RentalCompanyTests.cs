using FluentAssertions;
using ScooterRental;

namespace ScooterRental.Tests
{
    [TestClass]
    public class RentalCompanyTests
    {
        private IRentalCompany _company;
        private const string DEFAULT_COMPANY_NAME = "default";
        private const string DEFAULT_SCOOTER_ID = "1";
        private const decimal DEFAULT_PRICE = 1.0m;
        private List<Scooter> _scooterList;
        private List<RentedScooter> _rentedScooters;
        private CalculateRentPrice _calculateRentPrice;

        [TestInitialize]
        public void Setup()
        {
            _scooterList = new List<Scooter>();
            _rentedScooters = new List<RentedScooter>();
            _calculateRentPrice = new CalculateRentPrice();
            _company = new RentalCompany(
                DEFAULT_COMPANY_NAME,
                new ScooterService(_scooterList),
                _rentedScooters,
                _calculateRentPrice
                );
        }

        [TestMethod]
        public void Name_ShouldReturnCompanyName()
        {
            string companyName = _company.Name;

            companyName.Should().Be(DEFAULT_COMPANY_NAME);
        }

        [TestMethod]
        public void StartRent_AddsRentedScooterToList_RentStarted()
        {
            _scooterList.Add(new Scooter(DEFAULT_SCOOTER_ID, DEFAULT_PRICE));

            _company.StartRent(DEFAULT_SCOOTER_ID);

            _rentedScooters.Should().Contain(rent => rent.Id == DEFAULT_SCOOTER_ID);

            var scooter = _scooterList.Find(s => s.Id == DEFAULT_SCOOTER_ID);
            scooter.Should().NotBeNull();
            scooter.IsRented.Should().BeTrue();
        }

        [TestMethod]
        public void StartRent_ScooterAllreadyRented_ThrowsInvalidRentException()
        {
            _scooterList.Add(new Scooter(DEFAULT_SCOOTER_ID, DEFAULT_PRICE));
            _company.StartRent(DEFAULT_SCOOTER_ID);

            Action action = () => _company.StartRent(DEFAULT_SCOOTER_ID);

            action.Should().Throw<InvalidInvalidRentException>();
        }

        [TestMethod]
        public void EndRent_IvalidId_ThrowsInvalidIdException()
        {
            _scooterList.Add(new Scooter(DEFAULT_SCOOTER_ID, DEFAULT_PRICE));
            _rentedScooters.Add(new RentedScooter(DEFAULT_SCOOTER_ID, DateTime.Now.AddMinutes(-10)));

            Action action = () => _company.EndRent("");

            action.Should().Throw<InvalidIdException>();
        }

        [TestMethod]
        public void EndRent_InvalidRecord_ThrowsRentalRecordException()
        {
            _scooterList.Add(new Scooter(DEFAULT_SCOOTER_ID, DEFAULT_PRICE));
            _rentedScooters.Add(new RentedScooter("", DateTime.Now.AddMinutes(-10)));

            Action action = () => _company.EndRent(DEFAULT_SCOOTER_ID);

            action.Should().Throw<RentalRecordException>();
        }

        [TestMethod]
        public void EndRent_EndsRentCorrect_RentEnded()
        {
            _scooterList.Add(new Scooter(DEFAULT_SCOOTER_ID, DEFAULT_PRICE));
            _rentedScooters.Add(new RentedScooter(DEFAULT_SCOOTER_ID, DateTime.Now.AddMinutes(-10)));

            var price = _company.EndRent(DEFAULT_SCOOTER_ID);

            price.Should().Be(10.0m);
        }

        [TestMethod]
        public void EndRent_DayPriceLimitRentalReached_RentEnded()
        {
            _scooterList.Add(new Scooter(DEFAULT_SCOOTER_ID, DEFAULT_PRICE));
            _rentedScooters.Add(new RentedScooter(DEFAULT_SCOOTER_ID, DateTime.Now.AddMinutes(-21)));

            var price = _company.EndRent(DEFAULT_SCOOTER_ID);

            price.Should().Be(20.0m);
        }

        [TestMethod]
        public void EndRent_LongRental3daysReachMaxPrice_RentEnded()
        {
            _scooterList.Add(new Scooter(DEFAULT_SCOOTER_ID, DEFAULT_PRICE));
            _rentedScooters.Add(new RentedScooter(DEFAULT_SCOOTER_ID, DateTime.Now.AddDays(-3)));

            var price = _company.EndRent(DEFAULT_SCOOTER_ID);

            price.Should().Be(80.0m);
        }

        [TestMethod]
        public void CalculateIncome_YearAndNotCompleatedRantals_ShouldCalculateIncome()
        {
            _scooterList.Add(new Scooter(DEFAULT_SCOOTER_ID, DEFAULT_PRICE));
            _rentedScooters.Add(new RentedScooter(DEFAULT_SCOOTER_ID, DateTime.Now.AddMinutes(-10)));

            _company.StartRent(DEFAULT_SCOOTER_ID);

            var totalIncome = _company.CalculateIncome(2023, includeNotCompletedRentals: true);

            totalIncome.Should().Be(10.0m); 
        }

        [TestMethod]
        public void CalculateIncome_YearAndCompleatedRantals_ShouldCalculateIncome()
        {
            _scooterList.Add(new Scooter(DEFAULT_SCOOTER_ID, DEFAULT_PRICE));
            _rentedScooters.Add(new RentedScooter(DEFAULT_SCOOTER_ID, DateTime.Now.AddMinutes(-23)));
            _company.StartRent(DEFAULT_SCOOTER_ID);
            _company.EndRent(DEFAULT_SCOOTER_ID);
            

            var totalIncome = _company.CalculateIncome(2023, includeNotCompletedRentals: true);

            totalIncome.Should().Be(20.0m);
        }

        [TestMethod]
        public void CalculateIncome_YearAndManyRentals_ShouldCalculateIncome()
        {
            _scooterList.Add(new Scooter("1", DEFAULT_PRICE));
            _rentedScooters.Add(new RentedScooter("1", DateTime.Now.AddMinutes(-10)));
            _scooterList.Add(new Scooter("2", DEFAULT_PRICE));
            _rentedScooters.Add(new RentedScooter("2", DateTime.Now.AddMinutes(-28)));
            _company.StartRent("1");
            _company.EndRent("1");
            _company.StartRent("2");
            _company.EndRent("2");

            var totalIncome = _company.CalculateIncome(2023, includeNotCompletedRentals: true);

            totalIncome.Should().Be(30.0m);
        }
    }
}
