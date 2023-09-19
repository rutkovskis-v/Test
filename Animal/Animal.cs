using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Animal
{
    public abstract class Animal
    {
        protected string _animalName;
        protected string _animalType;
        protected double _animalWeight;
        protected double _foodEaten;

        public Animal(string name, string type, double weight)
        {
            _animalName = name;
            _animalType = type;
            _animalWeight = weight;
            if(_animalWeight <= 0)
                throw new NegativeWeightException();
            _foodEaten = 0;
        }

        public abstract void MakeSound();

        public abstract void Eat(Food food);

        public override string ToString()
        {
            return $"{_animalType} [{_animalName}, {_animalWeight.ToString("F1")}, {_foodEaten}]";
        }
    }
}
