using Animal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animal
{
    public class Meat : Food
    {
        public Meat(double quantity) : base(quantity)
        {
            if (quantity < 0)
                throw new NegativeQuantityExeption();
        }
    }
}
