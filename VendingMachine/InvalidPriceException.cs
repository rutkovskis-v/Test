namespace VendingMachine
{
    public class InvalidPriceException : Exception
    {
        public InvalidPriceException() : base("Price cannot be negative or zero") 
        {
        }
    }
}
