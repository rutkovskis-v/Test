using System;
using System.Collections.Generic;
using System.Text;

namespace Animal
{
    public abstract class Mammal : Animal
    {
        protected string _livingRegion;

        public Mammal(string name, string type, double weight, string region)
            : base(name, type, weight)
        {
            _livingRegion = region;
        }

        public override string ToString()
        {
            return $"{_animalType} [{_animalName}, {_animalWeight.ToString("F1")}, {_livingRegion}, {_foodEaten}]";
        }
    }
}
