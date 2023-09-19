namespace Animal
{
    public class Cat : Felime
    {
        public string Breed { get; }

        public Cat(string name, double weight, string region, string breed)
            : base(name, "Cat", weight, region)
        {
            Breed = breed;
        }

        public override void Eat(Food food)
        {
            if (food is Meat || food is Vegetable)
            {
                _foodEaten += food.Quantity;
            }
        }

        public override void MakeSound()
        {
            Console.WriteLine("Meooow");
        }
    }
}
