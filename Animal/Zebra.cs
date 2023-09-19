using Animal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Animal
{
    public class Zebra : Mammal
    {
        public Zebra(string name, double weight, string region)
            : base(name, "Zebra", weight, region) { }

        public override void Eat(Food food)
        {
            if (food is Vegetable)
            {
                _foodEaten += food.Quantity;
            }
            else
            {
                Console.WriteLine($"{GetType().Name}s are not eating that type of food!");
            }
        }

        public override void MakeSound()
        {
            Console.WriteLine("zzzzebrrr");
        }
    }
}
