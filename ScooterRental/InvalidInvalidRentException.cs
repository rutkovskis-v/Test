namespace ScooterRental
{
    public class InvalidInvalidRentException : Exception
    {
        public InvalidInvalidRentException() : base("Scooter is allready rented!")
        {
        }
    }
}
