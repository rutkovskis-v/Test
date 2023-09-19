using Animal;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Animal
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>();

            while (true)
            {
                Console.WriteLine("Enter animal information (Animal type,Animal name,Animal weight,Living region,[Cat breed]):");
                string animalType = Console.ReadLine();
                if (animalType == "End")
                    break;

                string[] animalInfo = animalType.Split();
                if (animalInfo.Length < 4)
                {
                    Console.WriteLine("Invalid format. Try again.");
                    continue;
                }

                string name = animalInfo[1];
                string type = animalInfo[0];
                double weight = double.Parse(animalInfo[2]);
                string livingRegion = animalInfo[3];

                string breed = null;
                if (type == "Cat" && animalInfo.Length > 4)
                {
                    breed = animalInfo[4];
                }

                Animal animal = null;

                switch (type)
                {
                    case "Mouse":
                        animal = new Mouse(name, weight, livingRegion);
                        break;
                    case "Cat":
                        animal = new Cat(name, weight, livingRegion, breed);
                        break;
                    case "Tiger":
                        animal = new Tiger(name, weight, livingRegion);
                        break;
                    case "Zebra":
                        animal = new Zebra(name, weight, livingRegion);
                        break;
                    default:
                        Console.WriteLine("Unknown animal type.");
                        break;
                }

                animal.MakeSound();

                Console.WriteLine("Enter food information (Food type,Quantity):");
                string[] foodInfo = Console.ReadLine().Split();
                string foodName = foodInfo[0];
                double quantity = double.Parse(foodInfo[1]);

                Food food = null;

                if (foodName == "Meat")
                {
                    food = new Meat(quantity);
                }
                else if (foodName == "Vegetable")
                {
                    food = new Vegetable(quantity);
                }

                animal.Eat(food);
                animals.Add(animal); 
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
}