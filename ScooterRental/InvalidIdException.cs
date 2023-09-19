namespace ScooterRental
{
    public class InvalidIdException : Exception
    {
        public InvalidIdException() : base("ID must be in correct format")
        {
        }
    }
}
