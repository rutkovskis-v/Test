namespace Animal
{
    public class Mouse : Mammal
    {
        public Mouse(string name, double weight, string region)
            : base(name, "Mouse", weight, region) { }

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
            Console.WriteLine("pipipi");
        }
    }
}
