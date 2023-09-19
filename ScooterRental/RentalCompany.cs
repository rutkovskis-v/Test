namespace ScooterRental
{
    public class RentalCompany : IRentalCompany
    {
        private readonly IScooterService _scooterService;
        private readonly List<RentedScooter> _rentedScooterList;
        private readonly CalculateRentPrice _calculateRentPrice;
        public RentalCompany(string name,
            IScooterService scooterService,
            List<RentedScooter> rentedScooterList,
            CalculateRentPrice calculateRentPrice) 

        {
            Name = name;
            _scooterService = scooterService;
            _rentedScooterList = rentedScooterList;
            _calculateRentPrice = calculateRentPrice;
        }

        public string Name { get; }
       
        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            decimal totalIncome = 0;

            if (year.HasValue)
            {              
                totalIncome = _rentedScooterList
                       .Where(s => s.RentStart.Year == year)
                       .Sum(s => _calculateRentPrice.CalculatePrice(s, _scooterService.GetScooterById(s.Id)));
            }
            else
            {
                totalIncome = _rentedScooterList                        
                        .Sum(s => _calculateRentPrice.CalculatePrice(s, _scooterService.GetScooterById(s.Id)));
            }

            return Math.Round(totalIncome, 2);
        } 
        public decimal EndRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);
            scooter.IsRented = false;
            var rentalRecord = _rentedScooterList
                .FirstOrDefault(s => s.Id == scooter.Id && !s.RentEnd.HasValue);
            if (rentalRecord != null)
            {
                rentalRecord.RentEnd = DateTime.Now;
            }
            else
            {
                throw new RentalRecordException();
            }
            return _calculateRentPrice.CalculatePrice(rentalRecord, scooter);
        }

        public void StartRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);
            if (scooter.IsRented == true)
            {
                throw new InvalidInvalidRentException();
            }
            else
            {
                scooter.IsRented = true;
                _rentedScooterList.Add(new RentedScooter(scooter.Id, DateTime.Now));
            }
        }
    }
}
