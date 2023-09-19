using System;
using System.Collections.Generic;
using System.Text;

namespace Animal
{
    public abstract class Felime : Mammal
    {
        public Felime(string name, string type, double weight, string region)
            : base(name, type, weight, region) 
        {
            if (weight <= 0)
                throw new NegativeWeightException();
        }
    }
}
