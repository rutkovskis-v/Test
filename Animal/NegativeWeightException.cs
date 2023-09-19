namespace Animal
{
    public class NegativeWeightException : Exception
    {
        public NegativeWeightException() : base("Animal wight cannot be negative")
        {
        }
    }
}
