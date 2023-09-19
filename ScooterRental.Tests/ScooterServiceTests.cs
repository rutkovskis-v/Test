using FluentAssertions;

namespace ScooterRental.Tests
{
    [TestClass]
    public class ScooterServiceTests
    {
        private IScooterService _scooterService;
        private List<Scooter> _scooterList;

        [TestInitialize]
        public void Setup()
        {
            _scooterList = new List<Scooter>();
            _scooterService = new ScooterService(_scooterList);
        }

        [TestMethod]
        public void AddScooter_WithIdAndPricePerMinute_ScooterAdded()
        {
            _scooterService.AddScooter("1", 0.2m);

            _scooterList.Count.Should().Be(1);
        }

        [TestMethod]
        public void AddScooter_WithId1AndPricePerMinute2_ScooterAddedWithId1AndPrice2()
        {
            _scooterService.AddScooter("1", 2m);

            var scooter = _scooterList[0];

            scooter.Id.Should().Be("1");
            scooter.PricePerMinute.Should().Be(2m);
        }

        [TestMethod]
        public void AddScooter_DuplicateScooter_ThrowsException()
        {
            _scooterList.Add(new Scooter("1", 2));

            Action action = () => _scooterService.AddScooter("1", 2);

            action.Should().Throw<ScooterDuplicateException>();
        }

        [TestMethod]
        public void AddScooter_AddScooterWithNegativePrice_ThrowsNegativePriceException()
        {
            Action action = () => _scooterService.AddScooter("1", -2);   

            action.Should().Throw<NegativePriceException>();
        }

        [TestMethod]
        public void AddScooter_AddScooterWithWithEmptyID_ThrowsInvalidIDException()
        {
            Action action = () => _scooterService.AddScooter("", 0.2m);

            action.Should().Throw<InvalidIdException>();
        }

        [TestMethod]
        public void RemoveScooter_WithIdAndPricePerMinute_ScooterRemoved()
        {
            _scooterList.Add(new Scooter("1", 2));

            _scooterService.RemoveScooter("1");

            _scooterList.Count.Should().Be(0);
        }

        [TestMethod]
        public void RemoveScooter_WithWithEmptyID_ThrowsInvalidIDException()
        {
            _scooterList.Add(new Scooter("1", 2));

            Action action = () => _scooterService.AddScooter("", 0.2m);

            action.Should().Throw<InvalidIdException>();
        }

        [TestMethod]
        public void GetScooterById_ValidIdProvided_ReturnsScooter()
        {
            _scooterList.Add(new Scooter("1", 2));

            Scooter scooter = _scooterService.GetScooterById("1");

            scooter.Should().NotBeNull();
            scooter.Id.Should().Be("1");            
        }

        [TestMethod]
        public void GetScooterById_InvalidIdIsProvided_ThrowsInvalidIDException()
        {
            _scooterList.Add(new Scooter("1", 2));

            Action action = () => _scooterService.GetScooterById("");

            action.Should().Throw<InvalidIdException>();
        }

        [TestMethod]
        public void GetScooters_ReturnsListOfScooters()
        {
            _scooterList.Add(new Scooter("1", 1));
            _scooterList.Add(new Scooter("2", 1));

            IList<Scooter> scooters = _scooterService.GetScooters();

            scooters.Should().NotBeNull();
            scooters.Should().HaveCount(2);
            scooters[0].Id.Should().Be("1");
            scooters[1].Id.Should().Be("2");
        }
    }
}