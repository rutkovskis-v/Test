namespace ScooterRental
{
    public class CalculateRentPrice
    {
        private MultyDayPriceCalculator _rentPriceCalculator = new MultyDayPriceCalculator();

        public decimal CalculatePrice(RentedScooter rentalRecord, Scooter scooter)
        {
            var rentEnd = rentalRecord.RentEnd ?? DateTime.Now;
            decimal dayMaxPrice = 20.0m;

            if (rentalRecord.RentStart.Date > rentEnd.Date)
            {
                throw new InvalidTimeException();
            }

            if (rentalRecord.RentStart.Date == rentEnd.Date)
            {
                var rentalMinutes = (rentEnd - rentalRecord.RentStart).TotalMinutes;
                var rentalPrice = (decimal)rentalMinutes * scooter.PricePerMinute;
                if (rentalPrice >= dayMaxPrice)
                {
                    return Math.Round(dayMaxPrice, 2);
                }
                if (rentalPrice < dayMaxPrice)
                {
                    return Math.Round(rentalPrice, 2);
                }
            }

            if (rentalRecord.RentStart.Date < rentEnd.Date)
            {
                return _rentPriceCalculator.CalculateDailyPrice(rentalRecord, scooter, rentEnd);
            }
            else throw new Exception("Start and end date must be valid");
        }
    }
}
