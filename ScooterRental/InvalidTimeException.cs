namespace ScooterRental
{
    public class InvalidTimeException : Exception
    {
        public InvalidTimeException() : base("Time format must be correct")
        { }
    }
}
