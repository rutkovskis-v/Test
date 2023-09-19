namespace ScooterRental
{
    public class MultyDayPriceCalculator
    {
        public decimal CalculateDailyPrice(RentedScooter rentalRecord, Scooter scooter, DateTime rentEnd)
        {
            var dailyRentMinutes = new List<double>();
            var startDate = rentalRecord.RentStart.Date;
            var endDate = rentEnd.Date;

            for (var currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
            {
                var dayStartTime = (currentDate == startDate) ? rentalRecord.RentStart.TimeOfDay : TimeSpan.Zero;
                var dayEndTime = (currentDate == endDate) ? rentEnd.TimeOfDay : TimeSpan.FromHours(23).Add(TimeSpan.FromMinutes(59));
                var dayDurationMinutes = (dayEndTime - dayStartTime).TotalMinutes;
                dailyRentMinutes.Add(dayDurationMinutes);
            }

            var totalRentPrice = dailyRentMinutes.Sum(day => Math.Min((decimal)day * scooter.PricePerMinute, 20.0m));
            return Math.Round(totalRentPrice, 2);
        }
    }
}
