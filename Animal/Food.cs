using System;
using System.Collections.Generic;
using System.Text;

namespace Animal
{
    public abstract class Food
    {
        public double Quantity { get; set; }

        public Food(double quantity)
        {
            Quantity = quantity;
        }
    }
}
