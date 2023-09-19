namespace ScooterRental
{
    public class ScooterDuplicateException : Exception
    {
        public ScooterDuplicateException() : base("Scooter already exists")
        { 
        }
    }
}
