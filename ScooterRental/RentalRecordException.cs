namespace ScooterRental
{
    public class RentalRecordException : Exception
    {
        public RentalRecordException() : base("Rental record must be valid")
        { }
    }
}
