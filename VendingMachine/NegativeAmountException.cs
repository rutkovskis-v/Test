namespace VendingMachine
{
    public class NegativeAmountException : Exception
    {
        public NegativeAmountException() : base("Amount cannot be negative") { }
    }
}
