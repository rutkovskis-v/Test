namespace VendingMachine
{
    public class InvalidNameException: Exception
    {
        public InvalidNameException() : base("VendingMashine must have name") 
        {
        }
    }
}
