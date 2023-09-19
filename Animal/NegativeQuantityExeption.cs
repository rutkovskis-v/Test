namespace Animal
{
    public class NegativeQuantityExeption : Exception
    {
        public NegativeQuantityExeption() : base("Cannot add negative food quantity!")
        {            
        }
    }
}
